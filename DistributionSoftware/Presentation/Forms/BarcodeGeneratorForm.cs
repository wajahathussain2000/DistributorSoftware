using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;

namespace DistributionSoftware.Presentation.Forms
{
    public partial class BarcodeGeneratorForm : Form
    {
        private string currentBarcodeText = "";
        private string currentBarcodeType = "Code128";
        private Bitmap barcodeImage = null;

        public BarcodeGeneratorForm()
        {
            InitializeComponent();
            GenerateBarcode();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form settings
            this.Text = "Barcode Generator - Distribution Software";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Header Panel
            Panel headerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(52, 73, 94)
            };

            Label headerLabel = new Label
            {
                Text = "📊 Barcode Generator",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 20),
                AutoSize = true
            };

            Button closeBtn = new Button
            {
                Text = "✕",
                Size = new Size(40, 40),
                Location = new Point(950, 20),
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

            // Input Group
            GroupBox inputGroup = new GroupBox
            {
                Text = "📝 Barcode Input",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(450, 300),
                Location = new Point(20, 20)
            };

            // Barcode Text
            Label barcodeTextLabel = new Label { Text = "🔍 Barcode Text:", Location = new Point(20, 30), AutoSize = true };
            TextBox barcodeTextBox = new TextBox { Name = "txtBarcodeText", Location = new Point(20, 55), Size = new Size(400, 25), Font = new Font("Segoe UI", 10) };
            barcodeTextBox.TextChanged += BarcodeTextBox_TextChanged;

            // Barcode Type
            Label typeLabel = new Label { Text = "🏷️ Barcode Type:", Location = new Point(20, 90), AutoSize = true };
            ComboBox typeCombo = new ComboBox { Name = "cmbBarcodeType", Location = new Point(20, 115), Size = new Size(200, 25), Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            typeCombo.Items.AddRange(new string[] { "Code128", "Code39", "EAN-13", "EAN-8", "UPC-A", "UPC-E", "QR Code", "DataMatrix" });
            typeCombo.SelectedIndex = 0;
            typeCombo.SelectedIndexChanged += TypeCombo_SelectedIndexChanged;

            // Barcode Size
            Label sizeLabel = new Label { Text = "📏 Size:", Location = new Point(240, 90), AutoSize = true };
            ComboBox sizeCombo = new ComboBox { Name = "cmbSize", Location = new Point(240, 115), Size = new Size(120, 25), Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            sizeCombo.Items.AddRange(new string[] { "Small", "Medium", "Large", "Extra Large" });
            sizeCombo.SelectedIndex = 1;
            sizeCombo.SelectedIndexChanged += SizeCombo_SelectedIndexChanged;

            // Generate Button
            Button generateBtn = new Button
            {
                Text = "🔄 Generate Barcode",
                Size = new Size(150, 35),
                Location = new Point(20, 150),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            generateBtn.Click += GenerateBtn_Click;

            // Auto Generate Checkbox
            CheckBox autoGenerateCheck = new CheckBox
            {
                Text = "🔄 Auto Generate on Text Change",
                Location = new Point(20, 200),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                Checked = true
            };
            autoGenerateCheck.CheckedChanged += AutoGenerateCheck_CheckedChanged;

            // Scanner Integration
            GroupBox scannerGroup = new GroupBox
            {
                Text = "📱 Scanner Integration",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(400, 120),
                Location = new Point(20, 240)
            };

            Button scanBtn = new Button
            {
                Text = "📱 Scan Barcode",
                Size = new Size(120, 30),
                Location = new Point(20, 30),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            scanBtn.Click += ScanBtn_Click;

            Button cameraBtn = new Button
            {
                Text = "📷 Use Camera",
                Size = new Size(120, 30),
                Location = new Point(150, 30),
                BackColor = Color.FromArgb(155, 89, 182),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            cameraBtn.Click += CameraBtn_Click;

            Label scannerStatusLabel = new Label
            {
                Text = "🔴 Scanner: Not Connected",
                Location = new Point(20, 70),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(231, 76, 60)
            };

            scannerGroup.Controls.AddRange(new Control[] { scanBtn, cameraBtn, scannerStatusLabel });

            inputGroup.Controls.AddRange(new Control[] { barcodeTextLabel, barcodeTextBox, typeLabel, typeCombo, sizeLabel, sizeCombo, 
                                                        generateBtn, autoGenerateCheck, scannerGroup });

            // Preview Group
            GroupBox previewGroup = new GroupBox
            {
                Text = "👁️ Barcode Preview",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(450, 400),
                Location = new Point(490, 20)
            };

            // Barcode Display Panel
            Panel barcodePanel = new Panel
            {
                Name = "pnlBarcodeDisplay",
                Location = new Point(20, 30),
                Size = new Size(410, 200),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            // Barcode Info
            Label infoLabel = new Label
            {
                Name = "lblBarcodeInfo",
                Text = "Barcode Information will appear here",
                Location = new Point(20, 250),
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(52, 73, 94)
            };

            // Action Buttons
            Button printBtn = new Button
            {
                Text = "🖨️ Print Barcode",
                Size = new Size(120, 35),
                Location = new Point(20, 290),
                BackColor = Color.FromArgb(230, 126, 34),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            printBtn.Click += PrintBtn_Click;

            Button saveBtn = new Button
            {
                Text = "💾 Save Image",
                Size = new Size(120, 35),
                Location = new Point(150, 290),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            saveBtn.Click += SaveBtn_Click;

            Button copyBtn = new Button
            {
                Text = "📋 Copy to Clipboard",
                Size = new Size(140, 35),
                Location = new Point(280, 290),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            copyBtn.Click += CopyBtn_Click;

            previewGroup.Controls.AddRange(new Control[] { barcodePanel, infoLabel, printBtn, saveBtn, copyBtn });

            // Batch Generation Group
            GroupBox batchGroup = new GroupBox
            {
                Text = "📦 Batch Generation",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(920, 200),
                Location = new Point(20, 440)
            };

            Label startLabel = new Label { Text = "🔢 Start Number:", Location = new Point(20, 30), AutoSize = true };
            TextBox startTextBox = new TextBox { Name = "txtStartNumber", Location = new Point(20, 55), Size = new Size(120, 25), Font = new Font("Segoe UI", 10) };

            Label countLabel = new Label { Text = "📊 Count:", Location = new Point(160, 30), AutoSize = true };
            TextBox countTextBox = new TextBox { Name = "txtCount", Location = new Point(160, 55), Size = new Size(120, 25), Font = new Font("Segoe UI", 10) };

            Label prefixLabel = new Label { Text = "🏷️ Prefix:", Location = new Point(300, 30), AutoSize = true };
            TextBox prefixTextBox = new TextBox { Name = "txtPrefix", Location = new Point(300, 55), Size = new Size(120, 25), Font = new Font("Segoe UI", 10) };

            Button batchGenerateBtn = new Button
            {
                Text = "🔄 Generate Batch",
                Size = new Size(140, 35),
                Location = new Point(20, 100),
                BackColor = Color.FromArgb(155, 89, 182),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            batchGenerateBtn.Click += BatchGenerateBtn_Click;

            Button exportBtn = new Button
            {
                Text = "📤 Export Batch",
                Size = new Size(140, 35),
                Location = new Point(180, 100),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            exportBtn.Click += ExportBtn_Click;

            batchGroup.Controls.AddRange(new Control[] { startLabel, startTextBox, countLabel, countTextBox, prefixLabel, prefixTextBox, 
                                                       batchGenerateBtn, exportBtn });

            // Add controls to content panel
            contentPanel.Controls.AddRange(new Control[] { inputGroup, previewGroup, batchGroup });

            // Add panels to form
            this.Controls.Add(headerPanel);
            this.Controls.Add(contentPanel);

            this.ResumeLayout(false);
        }

        private void BarcodeTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            currentBarcodeText = txt.Text;
            
            CheckBox autoGenerate = (CheckBox)this.Controls.Find("autoGenerateCheck", true)[0];
            if (autoGenerate.Checked)
            {
                GenerateBarcode();
            }
        }

        private void TypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            currentBarcodeType = cmb.SelectedItem.ToString();
            GenerateBarcode();
        }

        private void SizeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerateBarcode();
        }

        private void AutoGenerateCheck_CheckedChanged(object sender, EventArgs e)
        {
            // Auto generate functionality
        }

        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            GenerateBarcode();
        }

        private void GenerateBarcode()
        {
            if (string.IsNullOrEmpty(currentBarcodeText))
            {
                currentBarcodeText = "SAMPLE123";
            }

            try
            {
                // Create a simple barcode representation (in real implementation, use a barcode library)
                barcodeImage = CreateBarcodeImage(currentBarcodeText, currentBarcodeType);
                
                Panel barcodePanel = (Panel)this.Controls.Find("pnlBarcodeDisplay", true)[0];
                barcodePanel.BackgroundImage = barcodeImage;
                barcodePanel.BackgroundImageLayout = ImageLayout.Center;

                // Update info label
                Label infoLabel = (Label)this.Controls.Find("lblBarcodeInfo", true)[0];
                infoLabel.Text = $"Type: {currentBarcodeType} | Text: {currentBarcodeText} | Size: {GetSelectedSize()}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating barcode: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Bitmap CreateBarcodeImage(string text, string type)
        {
            // This is a simplified barcode generation
            // In a real implementation, you would use a barcode library like ZXing.Net
            Bitmap bmp = new Bitmap(400, 100);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                using (Font font = new Font("Arial", 12, FontStyle.Bold))
                {
                    g.DrawString($"[{type}] {text}", font, Brushes.Black, 10, 40);
                }
                
                // Draw simple barcode lines
                Random rand = new Random();
                for (int i = 0; i < text.Length * 3; i++)
                {
                    int height = rand.Next(20, 60);
                    g.FillRectangle(Brushes.Black, 10 + i * 2, 60, 1, height);
                }
            }
            return bmp;
        }

        private string GetSelectedSize()
        {
            ComboBox sizeCombo = (ComboBox)this.Controls.Find("cmbSize", true)[0];
            return sizeCombo.SelectedItem.ToString();
        }

        private void ScanBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Scanner integration would be implemented here.\nConnect a barcode scanner to automatically read barcodes.", 
                "Scanner Integration", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CameraBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Camera integration would be implemented here.\nUse device camera to scan barcodes.", 
                "Camera Integration", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if (barcodeImage != null)
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += (s, ev) =>
                {
                    ev.Graphics.DrawImage(barcodeImage, 50, 50);
                    ev.Graphics.DrawString($"Barcode: {currentBarcodeText}", new Font("Arial", 12), Brushes.Black, 50, 160);
                };
                pd.Print();
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (barcodeImage != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                sfd.FileName = $"Barcode_{currentBarcodeText}_{DateTime.Now:yyyyMMdd_HHmmss}";
                
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    barcodeImage.Save(sfd.FileName);
                    MessageBox.Show("Barcode image saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void CopyBtn_Click(object sender, EventArgs e)
        {
            if (barcodeImage != null)
            {
                Clipboard.SetImage(barcodeImage);
                MessageBox.Show("Barcode copied to clipboard!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BatchGenerateBtn_Click(object sender, EventArgs e)
        {
            TextBox startTextBox = (TextBox)this.Controls.Find("txtStartNumber", true)[0];
            TextBox countTextBox = (TextBox)this.Controls.Find("txtCount", true)[0];
            TextBox prefixTextBox = (TextBox)this.Controls.Find("txtPrefix", true)[0];

            if (int.TryParse(startTextBox.Text, out int start) && int.TryParse(countTextBox.Text, out int count))
            {
                string prefix = prefixTextBox.Text;
                MessageBox.Show($"Batch generation would create {count} barcodes starting from {prefix}{start}", 
                    "Batch Generation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files|*.xlsx|CSV Files|*.csv";
            sfd.FileName = $"BarcodeBatch_{DateTime.Now:yyyyMMdd_HHmmss}";
            
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Batch export functionality would be implemented here.", 
                    "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

