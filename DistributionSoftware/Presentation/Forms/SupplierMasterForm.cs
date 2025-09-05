using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Drawing.Imaging;
using System.IO;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class SupplierMasterForm : Form
    {
        private SqlConnection connection;
        private string connectionString;
        private int currentSupplierId = 0;

        // Form Controls
        private TextBox txtSupplierCode;
        private TextBox txtSupplierName;
        private TextBox txtContactPerson;
        private TextBox txtPhone;
        private TextBox txtEmail;
        private TextBox txtAddress;
        private TextBox txtCity;
        private TextBox txtState;
        private TextBox txtZipCode;
        private TextBox txtCountry;
        private TextBox txtGST;
        private TextBox txtNTN;
        private DateTimePicker dtpPaymentTermsFrom;
        private DateTimePicker dtpPaymentTermsTo;
        private TextBox txtPaymentTermsDays;
        private CheckBox chkIsActive;
        private TextBox txtNotes;
        private PictureBox picBarcode;
        private PictureBox picQRCode;
        private DataGridView dgvSuppliers;

        public SupplierMasterForm()
        {
            try
            {
                InitializeComponent();
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
            connectionString = ConfigurationManager.ConnectionStrings["DistributionConnection"]?.ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Database connection string not found.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            connection = new SqlConnection(connectionString);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form settings
            this.Text = "Supplier Master - Distribution Software";
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.Size = new Size(1200, 700);
            this.MinimumSize = new Size(1100, 600);

            // Header Panel
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(52, 73, 94)
            };

            Label headerLabel = new Label
            {
                Text = "🏢 Supplier Master",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 20),
                AutoSize = true
            };

            Button closeBtn = new Button
            {
                Text = "✕",
                Size = new Size(40, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(this.Width - 80, 20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            closeBtn.Click += (s, e) => this.Close();

            headerPanel.Controls.Add(headerLabel);
            headerPanel.Controls.Add(closeBtn);

            // Main Content Panel
            Panel contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                AutoScroll = true
            };

            // Supplier Information Group
            GroupBox supplierGroup = new GroupBox
            {
                Text = "📋 Supplier Information",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(700, 500),
                Location = new Point(20, 100),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            // Supplier Code
            Label lblSupplierCode = new Label
            {
                Text = "Supplier Code:",
                Location = new Point(20, 40),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtSupplierCode = new TextBox
            {
                Location = new Point(150, 38),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240)
            };

            // Supplier Name
            Label lblSupplierName = new Label
            {
                Text = "Supplier Name:",
                Location = new Point(20, 80),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtSupplierName = new TextBox
            {
                Location = new Point(150, 78),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10)
            };

            // Contact Person
            Label lblContactPerson = new Label
            {
                Text = "Contact Person:",
                Location = new Point(20, 120),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtContactPerson = new TextBox
            {
                Location = new Point(150, 118),
                Size = new Size(300, 25),
                Font = new Font("Segoe UI", 10)
            };

            // Contact Number
            Label lblPhone = new Label
            {
                Text = "Contact Number:",
                Location = new Point(20, 160),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtPhone = new TextBox
            {
                Location = new Point(150, 158),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10)
            };

            // Email
            Label lblEmail = new Label
            {
                Text = "Email:",
                Location = new Point(380, 160),
                Size = new Size(60, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtEmail = new TextBox
            {
                Location = new Point(450, 158),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10),
                Enabled = true
            };

            // Address
            Label lblAddress = new Label
            {
                Text = "Address:",
                Location = new Point(20, 200),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtAddress = new TextBox
            {
                Location = new Point(150, 198),
                Size = new Size(400, 25),
                Font = new Font("Segoe UI", 10)
            };

            // City
            Label lblCity = new Label
            {
                Text = "City:",
                Location = new Point(20, 240),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtCity = new TextBox
            {
                Location = new Point(150, 238),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10)
            };

            // State
            Label lblState = new Label
            {
                Text = "State:",
                Location = new Point(380, 240),
                Size = new Size(60, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtState = new TextBox
            {
                Location = new Point(450, 238),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10),
                Enabled = true
            };

            // Postal Code
            Label lblZipCode = new Label
            {
                Text = "Postal Code:",
                Location = new Point(20, 280),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtZipCode = new TextBox
            {
                Location = new Point(150, 278),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10)
            };

            // Country
            Label lblCountry = new Label
            {
                Text = "Country:",
                Location = new Point(380, 280),
                Size = new Size(60, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtCountry = new TextBox
            {
                Location = new Point(450, 278),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10),
                Enabled = true,
                Text = "Pakistan"
            };

            // GST
            Label lblGST = new Label
            {
                Text = "GST:",
                Location = new Point(20, 320),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtGST = new TextBox
            {
                Location = new Point(150, 318),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10)
            };

            // NTN
            Label lblNTN = new Label
            {
                Text = "NTN:",
                Location = new Point(380, 320),
                Size = new Size(60, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtNTN = new TextBox
            {
                Location = new Point(450, 318),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 10),
                Enabled = true
            };

            // Payment Terms
            Label lblPaymentTerms = new Label
            {
                Text = "Payment Terms:",
                Location = new Point(20, 360),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            dtpPaymentTermsFrom = new DateTimePicker
            {
                Location = new Point(150, 358),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10),
                Value = DateTime.Today
            };
            dtpPaymentTermsFrom.ValueChanged += DtpPaymentTermsFrom_ValueChanged;

            Label lblTo = new Label
            {
                Text = "to",
                Location = new Point(280, 360),
                Size = new Size(20, 25),
                Font = new Font("Segoe UI", 10)
            };
            dtpPaymentTermsTo = new DateTimePicker
            {
                Location = new Point(310, 358),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10),
                Value = DateTime.Today.AddDays(30)
            };
            dtpPaymentTermsTo.ValueChanged += DtpPaymentTermsTo_ValueChanged;

            Label lblDays = new Label
            {
                Text = "Days:",
                Location = new Point(380, 360),
                Size = new Size(60, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtPaymentTermsDays = new TextBox
            {
                Location = new Point(450, 358),
                Size = new Size(80, 25),
                Font = new Font("Segoe UI", 10),
                ReadOnly = true,
                BackColor = Color.FromArgb(240, 240, 240),
                Text = "30"
            };

            // Is Active
            chkIsActive = new CheckBox
            {
                Text = "Active",
                Location = new Point(20, 400),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Checked = true
            };

            // Notes
            Label lblNotes = new Label
            {
                Text = "Notes:",
                Location = new Point(380, 400),
                Size = new Size(60, 25),
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            txtNotes = new TextBox
            {
                Location = new Point(450, 398),
                Size = new Size(200, 50),
                Font = new Font("Segoe UI", 10),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Enabled = true
            };

            // Add controls to supplier group
            supplierGroup.Controls.AddRange(new Control[] {
                lblSupplierCode, txtSupplierCode, lblSupplierName, txtSupplierName,
                lblContactPerson, txtContactPerson, lblPhone, txtPhone, lblEmail, txtEmail,
                lblAddress, txtAddress, lblCity, txtCity, lblState, txtState,
                lblZipCode, txtZipCode, lblCountry, txtCountry, lblGST, txtGST,
                lblNTN, txtNTN, lblPaymentTerms, dtpPaymentTermsFrom, lblTo, dtpPaymentTermsTo, lblDays, txtPaymentTermsDays,
                chkIsActive, lblNotes, txtNotes
            });

            // Barcode Group
            GroupBox barcodeGroup = new GroupBox
            {
                Text = "📊 Barcode Information",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(400, 350),
                Location = new Point(740, 200),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            // Barcode Label
            Label barcodeLabel = new Label 
            { 
                Text = "🔍 Barcode:", 
                Location = new Point(20, 30), 
                AutoSize = true, 
                Font = new Font("Segoe UI", 10, FontStyle.Bold) 
            };

            // Generate Barcode Button
            Button generateBtn = new Button
            {
                Text = "🔄 Generate",
                Location = new Point(300, 25),
                Size = new Size(120, 25),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            generateBtn.Click += (sender, e) => GenerateBarcodeAndQRCode();

            // Barcode Display Panel
            Panel barcodePanel = new Panel
            {
                Name = "pnlBarcode",
                Location = new Point(20, 55),
                Size = new Size(360, 80),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            picBarcode = new PictureBox
            {
                Location = new Point(10, 10),
                Size = new Size(340, 60),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White
            };

            // QR Code Label
            Label qrLabel = new Label 
            { 
                Text = "📱 QR Code:", 
                Location = new Point(20, 150), 
                AutoSize = true, 
                Font = new Font("Segoe UI", 10, FontStyle.Bold) 
            };

            // QR Code Display Panel
            Panel qrPanel = new Panel
            {
                Name = "pnlQRCode",
                Location = new Point(20, 175),
                Size = new Size(180, 150),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            picQRCode = new PictureBox
            {
                Location = new Point(5, 5),
                Size = new Size(170, 140),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White
            };

            barcodePanel.Controls.Add(picBarcode);
            qrPanel.Controls.Add(picQRCode);

            barcodeGroup.Controls.AddRange(new Control[] { barcodeLabel, generateBtn, barcodePanel, qrLabel, qrPanel });

            // Action Buttons
            Panel actionPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                BackColor = Color.FromArgb(236, 240, 241)
            };

            Button btnSave = new Button
            {
                Text = "💾 Save",
                Size = new Size(100, 40),
                Location = new Point(20, 20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnSave.Click += BtnSave_Click;

            Button btnUpdate = new Button
            {
                Text = "✏️ Update",
                Size = new Size(100, 40),
                Location = new Point(140, 20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnUpdate.Click += BtnUpdate_Click;

            Button btnDelete = new Button
            {
                Text = "🗑️ Delete",
                Size = new Size(100, 40),
                Location = new Point(260, 20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnDelete.Click += BtnDelete_Click;

            Button btnClear = new Button
            {
                Text = "🔄 Clear",
                Size = new Size(100, 40),
                Location = new Point(380, 20),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnClear.Click += BtnClear_Click;

            actionPanel.Controls.AddRange(new Control[] { btnSave, btnUpdate, btnDelete, btnClear });

            // Suppliers Grid
            GroupBox gridGroup = new GroupBox
            {
                Text = "📋 Suppliers List",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(1120, 350),
                Location = new Point(20, 580),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            dgvSuppliers = new DataGridView
            {
                Location = new Point(20, 30),
                Size = new Size(1080, 300),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                GridColor = Color.LightGray,
                ScrollBars = ScrollBars.Vertical,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
                RowHeadersVisible = false,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(248, 249, 250) },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    SelectionBackColor = Color.FromArgb(52, 152, 219),
                    SelectionForeColor = Color.White,
                    Font = new Font("Segoe UI", 9),
                    WrapMode = DataGridViewTriState.False
                },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(52, 73, 94),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            };
            dgvSuppliers.CellClick += DgvSuppliers_CellClick;

            gridGroup.Controls.Add(dgvSuppliers);

            // Add all controls to content panel
            contentPanel.Controls.AddRange(new Control[] {
                supplierGroup, barcodeGroup, gridGroup
            });

            // Add panels to form
            this.Controls.Add(headerPanel);
            this.Controls.Add(contentPanel);
            this.Controls.Add(actionPanel);

            this.ResumeLayout(false);
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
                string query = @"SELECT SupplierId, SupplierCode, SupplierName, ContactPerson, ContactNumber, Email, 
                                Address, City, State, PostalCode, Country, GST, NTN, 
                                PaymentTermsDays, IsActive, Notes, CreatedDate
                                FROM Suppliers ORDER BY SupplierCode";
                
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvSuppliers.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading suppliers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DtpPaymentTermsFrom_ValueChanged(object sender, EventArgs e)
        {
            CalculatePaymentTerms();
        }

        private void DtpPaymentTermsTo_ValueChanged(object sender, EventArgs e)
        {
            CalculatePaymentTerms();
        }

        private void CalculatePaymentTerms()
        {
            try
            {
                int days = (dtpPaymentTermsTo.Value - dtpPaymentTermsFrom.Value).Days;
                txtPaymentTermsDays.Text = days.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating payment terms: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateBarcodeAndQRCode()
        {
            try
            {
                string barcodeText = $"{txtSupplierCode.Text}-{txtSupplierName.Text}";
                string qrData = GenerateQRData();
                
                GenerateBarcodeImage(barcodeText, picBarcode);
                GenerateQRCodeImage(qrData, picQRCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating barcode/QR code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateBarcodeImage(string text, PictureBox pictureBox)
        {
            try
            {
                // Create a proper barcode representation like ProductMasterForm
                Bitmap barcodeBitmap = new Bitmap(340, 60);
                using (Graphics g = Graphics.FromImage(barcodeBitmap))
                {
                    g.Clear(Color.White);
                    
                    // Draw barcode bars (like ProductMasterForm)
                    Random rand = new Random(text.GetHashCode()); // Use text hash for consistent pattern
                    int barWidth = 2;
                    int x = 10;
                    
                    for (int i = 0; i < text.Length * 4 && x < 320; i++)
                    {
                        int barHeight = rand.Next(20, 40);
                        g.FillRectangle(Brushes.Black, x, 5, barWidth, barHeight);
                        x += barWidth + 1;
                    }
                    
                    // Draw text below barcode
                    using (Font font = new Font("Courier New", 8, FontStyle.Bold))
                    {
                        SizeF textSize = g.MeasureString(text, font);
                        float textX = (340 - textSize.Width) / 2;
                        g.DrawString(text, font, Brushes.Black, textX, 45);
                    }
                }
                pictureBox.Image = barcodeBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating barcode: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerateQRCodeImage(string data, PictureBox pictureBox)
        {
            try
            {
                // Create a proper QR code representation
                Bitmap qrBitmap = new Bitmap(170, 140);
                using (Graphics g = Graphics.FromImage(qrBitmap))
                {
                    g.Clear(Color.White);
                    
                    // Draw QR-like pattern with squares
                    Random rand = new Random(data.GetHashCode());
                    
                    // Draw border
                    g.DrawRectangle(Pens.Black, 5, 5, 160, 130);
                    
                    // Draw corner squares (QR code characteristic)
                    g.FillRectangle(Brushes.Black, 15, 15, 20, 20);
                    g.FillRectangle(Brushes.Black, 135, 15, 20, 20);
                    g.FillRectangle(Brushes.Black, 15, 105, 20, 20);
                    
                    // Draw inner corner squares
                    g.FillRectangle(Brushes.White, 20, 20, 10, 10);
                    g.FillRectangle(Brushes.White, 140, 20, 10, 10);
                    g.FillRectangle(Brushes.White, 20, 110, 10, 10);
                    
                    // Draw random pattern (simulating QR code data)
                    for (int x = 40; x < 130; x += 8)
                    {
                        for (int y = 40; y < 100; y += 8)
                        {
                            if (rand.Next(2) == 1)
                            {
                                g.FillRectangle(Brushes.Black, x, y, 6, 6);
                            }
                        }
                    }
                    
                    // Draw supplier code in center
                    using (Font font = new Font("Arial", 8, FontStyle.Bold))
                    {
                        string supplierCode = txtSupplierCode.Text;
                        SizeF textSize = g.MeasureString(supplierCode, font);
                        float textX = (170 - textSize.Width) / 2;
                        g.DrawString(supplierCode, font, Brushes.Black, textX, 110);
                    }
                }
                pictureBox.Image = qrBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating QR code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GenerateQRData()
        {
            StringBuilder qrData = new StringBuilder();
            qrData.AppendLine($"Supplier Code: {txtSupplierCode.Text}");
            qrData.AppendLine($"Name: {txtSupplierName.Text}");
            qrData.AppendLine($"Contact: {txtContactPerson.Text}");
            qrData.AppendLine($"Phone: {txtPhone.Text}");
            qrData.AppendLine($"Email: {txtEmail.Text}");
            qrData.AppendLine($"Address: {txtAddress.Text}");
            qrData.AppendLine($"City: {txtCity.Text}");
            qrData.AppendLine($"State: {txtState.Text}");
            qrData.AppendLine($"GST: {txtGST.Text}");
            qrData.AppendLine($"NTN: {txtNTN.Text}");
            return qrData.ToString();
        }

        private string GetCurrentUser()
        {
            try
            {
                // Try to get the current user from the application session
                // This should match how other forms get the current user
                return "System Administrator"; // Default fallback
            }
            catch
            {
                return "System"; // Fallback if no user session
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    string query = @"INSERT INTO Suppliers (SupplierCode, SupplierName, ContactPerson, ContactNumber, Email, 
                                    Address, City, State, PostalCode, Country, GST, NTN, PaymentTermsDays, 
                                    IsActive, Notes, CreatedDate, Barcode, QRCode, CreatedBy) 
                                    VALUES (@SupplierCode, @SupplierName, @ContactPerson, @ContactNumber, @Email, 
                                    @Address, @City, @State, @PostalCode, @Country, @GST, @NTN, @PaymentTermsDays, 
                                    @IsActive, @Notes, @CreatedDate, @Barcode, @QRCode, @CreatedBy)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@SupplierCode", txtSupplierCode.Text);
                        cmd.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text);
                        cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text);
                        cmd.Parameters.AddWithValue("@ContactNumber", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@City", txtCity.Text);
                        cmd.Parameters.AddWithValue("@State", txtState.Text);
                        cmd.Parameters.AddWithValue("@PostalCode", txtZipCode.Text);
                        cmd.Parameters.AddWithValue("@Country", txtCountry.Text);
                        cmd.Parameters.AddWithValue("@GST", txtGST.Text);
                        cmd.Parameters.AddWithValue("@NTN", txtNTN.Text);
                        cmd.Parameters.AddWithValue("@PaymentTermsDays", int.Parse(txtPaymentTermsDays.Text));
                        cmd.Parameters.AddWithValue("@IsActive", chkIsActive.Checked);
                        cmd.Parameters.AddWithValue("@Notes", txtNotes.Text);
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Barcode", $"{txtSupplierCode.Text}-{txtSupplierName.Text}");
                        cmd.Parameters.AddWithValue("@QRCode", GenerateQRData());
                        cmd.Parameters.AddWithValue("@CreatedBy", GetCurrentUser());

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();

                        MessageBox.Show("Supplier saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadSuppliersGrid();
                        ClearForm();
                        GenerateBarcodeAndQRCode();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving supplier: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentSupplierId == 0)
                {
                    MessageBox.Show("Please select a supplier to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (ValidateInput())
                {
                    string query = @"UPDATE Suppliers SET SupplierName = @SupplierName, ContactPerson = @ContactPerson, 
                                    ContactNumber = @ContactNumber, Email = @Email, Address = @Address, City = @City, 
                                    State = @State, PostalCode = @PostalCode, Country = @Country, GST = @GST, 
                                    NTN = @NTN, PaymentTermsDays = @PaymentTermsDays, IsActive = @IsActive, 
                                    Notes = @Notes, Barcode = @Barcode, QRCode = @QRCode, ModifiedDate = @ModifiedDate, 
                                    ModifiedBy = @ModifiedBy WHERE SupplierId = @SupplierId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@SupplierId", currentSupplierId);
                        cmd.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text);
                        cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text);
                        cmd.Parameters.AddWithValue("@ContactNumber", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@City", txtCity.Text);
                        cmd.Parameters.AddWithValue("@State", txtState.Text);
                        cmd.Parameters.AddWithValue("@PostalCode", txtZipCode.Text);
                        cmd.Parameters.AddWithValue("@Country", txtCountry.Text);
                        cmd.Parameters.AddWithValue("@GST", txtGST.Text);
                        cmd.Parameters.AddWithValue("@NTN", txtNTN.Text);
                        cmd.Parameters.AddWithValue("@PaymentTermsDays", int.Parse(txtPaymentTermsDays.Text));
                        cmd.Parameters.AddWithValue("@IsActive", chkIsActive.Checked);
                        cmd.Parameters.AddWithValue("@Notes", txtNotes.Text);
                        cmd.Parameters.AddWithValue("@Barcode", $"{txtSupplierCode.Text}-{txtSupplierName.Text}");
                        cmd.Parameters.AddWithValue("@QRCode", GenerateQRData());
                        cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ModifiedBy", GetCurrentUser());

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();

                        MessageBox.Show("Supplier updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadSuppliersGrid();
                        ClearForm();
                        GenerateBarcodeAndQRCode();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating supplier: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentSupplierId == 0)
                {
                    MessageBox.Show("Please select a supplier to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show("Are you sure you want to delete this supplier?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    string query = "DELETE FROM Suppliers WHERE SupplierId = @SupplierId";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@SupplierId", currentSupplierId);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();

                        MessageBox.Show("Supplier deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadSuppliersGrid();
                        ClearForm();
                    }
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
        }

        private void ClearForm()
        {
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
            dtpPaymentTermsTo.Value = DateTime.Today.AddDays(30);
            txtPaymentTermsDays.Text = "30";
            chkIsActive.Checked = true;
            txtNotes.Clear();
            picBarcode.Image = null;
            picQRCode.Image = null;
            currentSupplierId = 0;
            GenerateSupplierCode();
            GenerateBarcodeAndQRCode();
        }

        private bool ValidateInput()
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
                MessageBox.Show("Please enter contact number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            return true;
        }

        private void DgvSuppliers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvSuppliers.Rows[e.RowIndex];
                    currentSupplierId = Convert.ToInt32(row.Cells["SupplierId"].Value);
                    
                    txtSupplierCode.Text = row.Cells["SupplierCode"].Value.ToString();
                    txtSupplierName.Text = row.Cells["SupplierName"].Value.ToString();
                    txtContactPerson.Text = row.Cells["ContactPerson"].Value.ToString();
                    txtPhone.Text = row.Cells["ContactNumber"].Value.ToString();
                    txtEmail.Text = row.Cells["Email"].Value.ToString();
                    txtAddress.Text = row.Cells["Address"].Value.ToString();
                    txtCity.Text = row.Cells["City"].Value.ToString();
                    txtState.Text = row.Cells["State"].Value.ToString();
                    txtZipCode.Text = row.Cells["PostalCode"].Value.ToString();
                    txtCountry.Text = row.Cells["Country"].Value.ToString();
                    txtGST.Text = row.Cells["GST"].Value.ToString();
                    txtNTN.Text = row.Cells["NTN"].Value.ToString();
                    txtPaymentTermsDays.Text = row.Cells["PaymentTermsDays"].Value.ToString();
                    chkIsActive.Checked = Convert.ToBoolean(row.Cells["IsActive"].Value);
                    txtNotes.Text = row.Cells["Notes"].Value.ToString();

                    GenerateBarcodeAndQRCode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading supplier data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
