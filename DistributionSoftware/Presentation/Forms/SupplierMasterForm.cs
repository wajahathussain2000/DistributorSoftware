using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Drawing.Imaging;
using System.IO;
using DistributionSoftware.Common;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class SupplierMasterForm : Form
    {
        private SqlConnection connection;
        private string connectionString;
        private int currentSupplierId = 0;

        public SupplierMasterForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            try
            {
                InitializeConnection();
                GenerateSupplierCode();
                LoadSuppliersGrid();
                GenerateBarcodeAndQRCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing Supplier Master Form: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeConnection()
        {
            connectionString = DistributionSoftware.Common.ConfigurationManager.DistributionConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Database connection string not found.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            connection = new SqlConnection(connectionString);
        }

        private void GenerateSupplierCode()
        {
            try
            {
                string query = "SELECT ISNULL(MAX(CAST(SUBSTRING(SupplierCode, 4, LEN(SupplierCode)) AS INT)), 0) + 1 FROM Suppliers WHERE SupplierCode LIKE 'SUP%'";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    int nextNumber = Convert.ToInt32(cmd.ExecuteScalar());
                    txtSupplierCode.Text = $"SUP{nextNumber:D6}";
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating supplier code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSuppliersGrid()
        {
            try
            {
                string query = "SELECT SupplierId, SupplierCode, SupplierName, ContactPerson, Phone, Email, City, State, IsActive FROM Suppliers ORDER BY SupplierName";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvSuppliers.DataSource = dt;
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading suppliers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateBarcodeAndQRCode()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSupplierCode.Text))
                {
                    // Generate barcode
                    GenerateBarcode(txtSupplierCode.Text, picBarcode);
                    
                    // Generate QR code
                    GenerateQRCode(txtSupplierCode.Text, picQRCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating barcode/QR code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateBarcode(string text, PictureBox pictureBox)
        {
            try
            {
                // Simple barcode generation (you can replace with actual barcode library)
                Bitmap bmp = new Bitmap(200, 50);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                    using (Font font = new Font("Arial", 8))
                    {
                        g.DrawString(text, font, Brushes.Black, 10, 20);
                    }
                }
                pictureBox.Image = bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating barcode: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateQRCode(string text, PictureBox pictureBox)
        {
            try
            {
                // Simple QR code generation (you can replace with actual QR code library)
                Bitmap bmp = new Bitmap(100, 100);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                    using (Font font = new Font("Arial", 6))
                    {
                        g.DrawString(text, font, Brushes.Black, 10, 40);
                    }
                }
                pictureBox.Image = bmp;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating QR code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateForm())
                {
                    if (currentSupplierId == 0)
                    {
                        InsertSupplier();
                    }
                    else
                    {
                        UpdateSupplier();
                    }
                        LoadSuppliersGrid();
                        ClearForm();
                    GenerateSupplierCode();
                        GenerateBarcodeAndQRCode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving supplier: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentSupplierId > 0)
                {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this supplier?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                        DeleteSupplier();
                        LoadSuppliersGrid();
                        ClearForm();
                        GenerateSupplierCode();
                        GenerateBarcodeAndQRCode();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a supplier to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting supplier: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            GenerateSupplierCode();
            GenerateBarcodeAndQRCode();
        }

        private void SuppliersGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvSuppliers.Rows[e.RowIndex];
                    currentSupplierId = Convert.ToInt32(row.Cells["SupplierId"].Value);
                    LoadSupplierDetails();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading supplier details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                MessageBox.Show("Please enter supplier name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSupplierName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtContactPerson.Text))
            {
                MessageBox.Show("Please enter contact person.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContactPerson.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Please enter phone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            return true;
        }

        private void InsertSupplier()
        {
            string query = @"INSERT INTO Suppliers (SupplierCode, SupplierName, ContactPerson, Phone, Email, Address, City, State, PostalCode, Country, GST, NTN, PaymentTermsDate, PaymentTermsDays, IsActive, Notes, CreatedDate) 
                            VALUES (@SupplierCode, @SupplierName, @ContactPerson, @Phone, @Email, @Address, @City, @State, @PostalCode, @Country, @GST, @NTN, @PaymentTermsDate, @PaymentTermsDays, @IsActive, @Notes, @CreatedDate)";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@SupplierCode", txtSupplierCode.Text);
                cmd.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text);
                cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@City", txtCity.Text);
                cmd.Parameters.AddWithValue("@State", txtState.Text);
                cmd.Parameters.AddWithValue("@PostalCode", txtZipCode.Text);
                cmd.Parameters.AddWithValue("@Country", txtCountry.Text);
                cmd.Parameters.AddWithValue("@GST", txtGST.Text);
                cmd.Parameters.AddWithValue("@NTN", txtNTN.Text);
                cmd.Parameters.AddWithValue("@PaymentTermsDate", dtpPaymentTermsFrom.Value);
                cmd.Parameters.AddWithValue("@PaymentTermsDays", txtPaymentTermsDays.Text);
                cmd.Parameters.AddWithValue("@IsActive", chkIsActive.Checked);
                cmd.Parameters.AddWithValue("@Notes", txtNotes.Text);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Supplier added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateSupplier()
        {
            string query = @"UPDATE Suppliers SET SupplierName = @SupplierName, ContactPerson = @ContactPerson, Phone = @Phone, Email = @Email, Address = @Address, City = @City, State = @State, PostalCode = @PostalCode, Country = @Country, GST = @GST, NTN = @NTN, PaymentTermsDate = @PaymentTermsDate, PaymentTermsDays = @PaymentTermsDays, IsActive = @IsActive, Notes = @Notes, ModifiedDate = @ModifiedDate WHERE SupplierId = @SupplierId";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@SupplierId", currentSupplierId);
                cmd.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text);
                cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@City", txtCity.Text);
                cmd.Parameters.AddWithValue("@State", txtState.Text);
                cmd.Parameters.AddWithValue("@PostalCode", txtZipCode.Text);
                cmd.Parameters.AddWithValue("@Country", txtCountry.Text);
                cmd.Parameters.AddWithValue("@GST", txtGST.Text);
                cmd.Parameters.AddWithValue("@NTN", txtNTN.Text);
                cmd.Parameters.AddWithValue("@PaymentTermsDate", dtpPaymentTermsFrom.Value);
                cmd.Parameters.AddWithValue("@PaymentTermsDays", txtPaymentTermsDays.Text);
                cmd.Parameters.AddWithValue("@IsActive", chkIsActive.Checked);
                cmd.Parameters.AddWithValue("@Notes", txtNotes.Text);
                cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Supplier updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeleteSupplier()
        {
            string query = "DELETE FROM Suppliers WHERE SupplierId = @SupplierId";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@SupplierId", currentSupplierId);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Supplier deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadSupplierDetails()
        {
            try
            {
                string query = "SELECT * FROM Suppliers WHERE SupplierId = @SupplierId";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@SupplierId", currentSupplierId);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtSupplierCode.Text = reader["SupplierCode"].ToString();
                        txtSupplierName.Text = reader["SupplierName"].ToString();
                        txtContactPerson.Text = reader["ContactPerson"].ToString();
                        txtPhone.Text = reader["Phone"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtAddress.Text = reader["Address"].ToString();
                        txtCity.Text = reader["City"].ToString();
                        txtState.Text = reader["State"].ToString();
                        txtZipCode.Text = reader["PostalCode"].ToString();
                        txtCountry.Text = reader["Country"].ToString();
                        txtGST.Text = reader["GST"].ToString();
                        txtNTN.Text = reader["NTN"].ToString();
                        
                        // Handle PaymentTermsDate (single field) instead of separate From/To fields
                        if (reader["PaymentTermsDate"] != DBNull.Value)
                        {
                            DateTime paymentDate = Convert.ToDateTime(reader["PaymentTermsDate"]);
                            dtpPaymentTermsFrom.Value = paymentDate;
                            dtpPaymentTermsTo.Value = paymentDate;
                        }
                        
                        txtPaymentTermsDays.Text = reader["PaymentTermsDays"].ToString();
                        chkIsActive.Checked = Convert.ToBoolean(reader["IsActive"]);
                        txtNotes.Text = reader["Notes"].ToString();
                    }
                    reader.Close();
                    connection.Close();
                }
                    GenerateBarcodeAndQRCode();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading supplier details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            currentSupplierId = 0;
            txtSupplierCode.Clear();
            txtSupplierName.Clear();
            txtContactPerson.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtCity.Clear();
            txtState.Clear();
            txtZipCode.Clear();
            txtCountry.Clear();
            txtGST.Clear();
            txtNTN.Clear();
            dtpPaymentTermsFrom.Value = DateTime.Today;
            dtpPaymentTermsTo.Value = DateTime.Today;
            txtPaymentTermsDays.Clear();
            chkIsActive.Checked = true;
            txtNotes.Clear();
            picBarcode.Image = null;
            picQRCode.Image = null;
        }

    }
}
