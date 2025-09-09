namespace DistributionSoftware.Presentation.Forms
{
    partial class GRNForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblGRNNo = new System.Windows.Forms.Label();
            this.txtGRNNo = new System.Windows.Forms.TextBox();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.picBarcode = new System.Windows.Forms.PictureBox();
            this.lblPurchase = new System.Windows.Forms.Label();
            this.cmbPurchase = new System.Windows.Forms.ComboBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.lblGRNDate = new System.Windows.Forms.Label();
            this.dtpGRNDate = new System.Windows.Forms.DateTimePicker();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.groupBoxItems = new System.Windows.Forms.GroupBox();
            this.dgvGRNItems = new System.Windows.Forms.DataGridView();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.dtpExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.lblExpiryDate = new System.Windows.Forms.Label();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.lblBatchNo = new System.Windows.Forms.Label();
            this.txtAcceptedQty = new System.Windows.Forms.TextBox();
            this.lblAcceptedQty = new System.Windows.Forms.Label();
            this.txtReceivedQty = new System.Windows.Forms.TextBox();
            this.lblReceivedQty = new System.Windows.Forms.Label();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPost = new System.Windows.Forms.Button();
            this.btnSaveDraft = new System.Windows.Forms.Button();
            this.groupBoxGRNList = new System.Windows.Forms.GroupBox();
            this.dgvGRNList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).BeginInit();
            this.groupBoxItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGRNItems)).BeginInit();
            this.groupBoxActions.SuspendLayout();
            this.groupBoxGRNList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGRNList)).BeginInit();
            this.SuspendLayout();
            // 
            // lblGRNNo
            // 
            this.lblGRNNo.AutoSize = true;
            this.lblGRNNo.Location = new System.Drawing.Point(30, 31);
            this.lblGRNNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGRNNo.Name = "lblGRNNo";
            this.lblGRNNo.Size = new System.Drawing.Size(73, 20);
            this.lblGRNNo.TabIndex = 0;
            this.lblGRNNo.Text = "GRN No:";
            // 
            // txtGRNNo
            // 
            this.txtGRNNo.Location = new System.Drawing.Point(150, 26);
            this.txtGRNNo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtGRNNo.Name = "txtGRNNo";
            this.txtGRNNo.ReadOnly = true;
            this.txtGRNNo.Size = new System.Drawing.Size(298, 26);
            this.txtGRNNo.TabIndex = 1;
            // 
            // lblBarcode
            // 
            this.lblBarcode.AutoSize = true;
            this.lblBarcode.Location = new System.Drawing.Point(480, 31);
            this.lblBarcode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(73, 20);
            this.lblBarcode.TabIndex = 2;
            this.lblBarcode.Text = "Barcode:";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(600, 26);
            this.txtBarcode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.ReadOnly = true;
            this.txtBarcode.Size = new System.Drawing.Size(298, 26);
            this.txtBarcode.TabIndex = 3;
            // 
            // picBarcode
            // 
            this.picBarcode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBarcode.Location = new System.Drawing.Point(930, 15);
            this.picBarcode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picBarcode.Name = "picBarcode";
            this.picBarcode.Size = new System.Drawing.Size(449, 122);
            this.picBarcode.TabIndex = 4;
            this.picBarcode.TabStop = false;
            // 
            // lblPurchase
            // 
            this.lblPurchase.AutoSize = true;
            this.lblPurchase.Location = new System.Drawing.Point(30, 92);
            this.lblPurchase.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPurchase.Name = "lblPurchase";
            this.lblPurchase.Size = new System.Drawing.Size(80, 20);
            this.lblPurchase.TabIndex = 5;
            this.lblPurchase.Text = "Purchase:";
            // 
            // cmbPurchase
            // 
            this.cmbPurchase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPurchase.FormattingEnabled = true;
            this.cmbPurchase.Location = new System.Drawing.Point(150, 88);
            this.cmbPurchase.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbPurchase.Name = "cmbPurchase";
            this.cmbPurchase.Size = new System.Drawing.Size(298, 28);
            this.cmbPurchase.TabIndex = 6;
            this.cmbPurchase.SelectedIndexChanged += new System.EventHandler(this.CmbPurchase_SelectedIndexChanged);
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(480, 92);
            this.lblSupplier.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(71, 20);
            this.lblSupplier.TabIndex = 7;
            this.lblSupplier.Text = "Supplier:";
            // 
            // txtSupplier
            // 
            this.txtSupplier.Location = new System.Drawing.Point(600, 88);
            this.txtSupplier.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.ReadOnly = true;
            this.txtSupplier.Size = new System.Drawing.Size(298, 26);
            this.txtSupplier.TabIndex = 8;
            // 
            // lblGRNDate
            // 
            this.lblGRNDate.AutoSize = true;
            this.lblGRNDate.Location = new System.Drawing.Point(30, 154);
            this.lblGRNDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGRNDate.Name = "lblGRNDate";
            this.lblGRNDate.Size = new System.Drawing.Size(88, 20);
            this.lblGRNDate.TabIndex = 9;
            this.lblGRNDate.Text = "GRN Date:";
            // 
            // dtpGRNDate
            // 
            this.dtpGRNDate.Location = new System.Drawing.Point(150, 149);
            this.dtpGRNDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpGRNDate.Name = "dtpGRNDate";
            this.dtpGRNDate.Size = new System.Drawing.Size(298, 26);
            this.dtpGRNDate.TabIndex = 10;
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Location = new System.Drawing.Point(480, 154);
            this.lblRemarks.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(77, 20);
            this.lblRemarks.TabIndex = 11;
            this.lblRemarks.Text = "Remarks:";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(600, 149);
            this.txtRemarks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(298, 75);
            this.txtRemarks.TabIndex = 12;
            // 
            // groupBoxItems
            // 
            this.groupBoxItems.Controls.Add(this.dgvGRNItems);
            this.groupBoxItems.Controls.Add(this.btnRemoveItem);
            this.groupBoxItems.Controls.Add(this.btnAddItem);
            this.groupBoxItems.Controls.Add(this.dtpExpiryDate);
            this.groupBoxItems.Controls.Add(this.lblExpiryDate);
            this.groupBoxItems.Controls.Add(this.txtBatchNo);
            this.groupBoxItems.Controls.Add(this.lblBatchNo);
            this.groupBoxItems.Controls.Add(this.txtAcceptedQty);
            this.groupBoxItems.Controls.Add(this.lblAcceptedQty);
            this.groupBoxItems.Controls.Add(this.txtReceivedQty);
            this.groupBoxItems.Controls.Add(this.lblReceivedQty);
            this.groupBoxItems.Controls.Add(this.cmbProduct);
            this.groupBoxItems.Controls.Add(this.lblProduct);
            this.groupBoxItems.Location = new System.Drawing.Point(30, 231);
            this.groupBoxItems.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxItems.Name = "groupBoxItems";
            this.groupBoxItems.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxItems.Size = new System.Drawing.Size(1350, 462);
            this.groupBoxItems.TabIndex = 13;
            this.groupBoxItems.TabStop = false;
            this.groupBoxItems.Text = "GRN Items";
            // 
            // dgvGRNItems
            // 
            this.dgvGRNItems.AllowUserToAddRows = false;
            this.dgvGRNItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGRNItems.Location = new System.Drawing.Point(30, 138);
            this.dgvGRNItems.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvGRNItems.Name = "dgvGRNItems";
            this.dgvGRNItems.RowHeadersWidth = 62;
            this.dgvGRNItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGRNItems.Size = new System.Drawing.Size(1290, 308);
            this.dgvGRNItems.TabIndex = 12;
            this.dgvGRNItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvGRNItems_CellClick);
            this.dgvGRNItems.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvGRNItems_CellValueChanged);
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.Location = new System.Drawing.Point(1065, 80);
            this.btnRemoveItem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(112, 38);
            this.btnRemoveItem.TabIndex = 11;
            this.btnRemoveItem.Text = "Remove";
            this.btnRemoveItem.UseVisualStyleBackColor = true;
            this.btnRemoveItem.Click += new System.EventHandler(this.BtnRemoveItem_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(930, 82);
            this.btnAddItem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(112, 38);
            this.btnAddItem.TabIndex = 10;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.BtnAddItem_Click);
            // 
            // dtpExpiryDate
            // 
            this.dtpExpiryDate.Location = new System.Drawing.Point(600, 88);
            this.dtpExpiryDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpExpiryDate.Name = "dtpExpiryDate";
            this.dtpExpiryDate.Size = new System.Drawing.Size(298, 26);
            this.dtpExpiryDate.TabIndex = 9;
            // 
            // lblExpiryDate
            // 
            this.lblExpiryDate.AutoSize = true;
            this.lblExpiryDate.Location = new System.Drawing.Point(480, 92);
            this.lblExpiryDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExpiryDate.Name = "lblExpiryDate";
            this.lblExpiryDate.Size = new System.Drawing.Size(94, 20);
            this.lblExpiryDate.TabIndex = 8;
            this.lblExpiryDate.Text = "Expiry Date:";
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Location = new System.Drawing.Point(150, 88);
            this.txtBatchNo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.Size = new System.Drawing.Size(298, 26);
            this.txtBatchNo.TabIndex = 7;
            // 
            // lblBatchNo
            // 
            this.lblBatchNo.AutoSize = true;
            this.lblBatchNo.Location = new System.Drawing.Point(30, 92);
            this.lblBatchNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBatchNo.Name = "lblBatchNo";
            this.lblBatchNo.Size = new System.Drawing.Size(79, 20);
            this.lblBatchNo.TabIndex = 6;
            this.lblBatchNo.Text = "Batch No:";
            // 
            // txtAcceptedQty
            // 
            this.txtAcceptedQty.Location = new System.Drawing.Point(900, 42);
            this.txtAcceptedQty.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAcceptedQty.Name = "txtAcceptedQty";
            this.txtAcceptedQty.Size = new System.Drawing.Size(148, 26);
            this.txtAcceptedQty.TabIndex = 5;
            // 
            // lblAcceptedQty
            // 
            this.lblAcceptedQty.AutoSize = true;
            this.lblAcceptedQty.Location = new System.Drawing.Point(780, 46);
            this.lblAcceptedQty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAcceptedQty.Name = "lblAcceptedQty";
            this.lblAcceptedQty.Size = new System.Drawing.Size(109, 20);
            this.lblAcceptedQty.TabIndex = 4;
            this.lblAcceptedQty.Text = "Accepted Qty:";
            // 
            // txtReceivedQty
            // 
            this.txtReceivedQty.Location = new System.Drawing.Point(600, 42);
            this.txtReceivedQty.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtReceivedQty.Name = "txtReceivedQty";
            this.txtReceivedQty.Size = new System.Drawing.Size(148, 26);
            this.txtReceivedQty.TabIndex = 3;
            // 
            // lblReceivedQty
            // 
            this.lblReceivedQty.AutoSize = true;
            this.lblReceivedQty.Location = new System.Drawing.Point(480, 46);
            this.lblReceivedQty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReceivedQty.Name = "lblReceivedQty";
            this.lblReceivedQty.Size = new System.Drawing.Size(107, 20);
            this.lblReceivedQty.TabIndex = 2;
            this.lblReceivedQty.Text = "Received Qty:";
            // 
            // cmbProduct
            // 
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.Location = new System.Drawing.Point(150, 42);
            this.cmbProduct.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(298, 28);
            this.cmbProduct.TabIndex = 1;
            this.cmbProduct.SelectedIndexChanged += new System.EventHandler(this.CmbProduct_SelectedIndexChanged);
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(30, 46);
            this.lblProduct.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(68, 20);
            this.lblProduct.TabIndex = 0;
            this.lblProduct.Text = "Product:";
            // 
            // groupBoxActions
            // 
            this.groupBoxActions.Controls.Add(this.btnClear);
            this.groupBoxActions.Controls.Add(this.btnPrint);
            this.groupBoxActions.Controls.Add(this.btnPost);
            this.groupBoxActions.Controls.Add(this.btnSaveDraft);
            this.groupBoxActions.Location = new System.Drawing.Point(30, 723);
            this.groupBoxActions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxActions.Size = new System.Drawing.Size(1350, 92);
            this.groupBoxActions.TabIndex = 14;
            this.groupBoxActions.TabStop = false;
            this.groupBoxActions.Text = "Actions";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(570, 38);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(150, 38);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(390, 38);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(150, 38);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // btnPost
            // 
            this.btnPost.Location = new System.Drawing.Point(210, 38);
            this.btnPost.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPost.Name = "btnPost";
            this.btnPost.Size = new System.Drawing.Size(150, 38);
            this.btnPost.TabIndex = 1;
            this.btnPost.Text = "Post";
            this.btnPost.UseVisualStyleBackColor = true;
            this.btnPost.Click += new System.EventHandler(this.BtnPost_Click);
            // 
            // btnSaveDraft
            // 
            this.btnSaveDraft.Location = new System.Drawing.Point(30, 38);
            this.btnSaveDraft.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSaveDraft.Name = "btnSaveDraft";
            this.btnSaveDraft.Size = new System.Drawing.Size(150, 38);
            this.btnSaveDraft.TabIndex = 0;
            this.btnSaveDraft.Text = "Save Draft";
            this.btnSaveDraft.UseVisualStyleBackColor = true;
            this.btnSaveDraft.Click += new System.EventHandler(this.BtnSaveDraft_Click);
            // 
            // groupBoxGRNList
            // 
            this.groupBoxGRNList.Controls.Add(this.dgvGRNList);
            this.groupBoxGRNList.Location = new System.Drawing.Point(30, 846);
            this.groupBoxGRNList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxGRNList.Name = "groupBoxGRNList";
            this.groupBoxGRNList.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBoxGRNList.Size = new System.Drawing.Size(1350, 308);
            this.groupBoxGRNList.TabIndex = 15;
            this.groupBoxGRNList.TabStop = false;
            this.groupBoxGRNList.Text = "GRN List";
            // 
            // dgvGRNList
            // 
            this.dgvGRNList.AllowUserToAddRows = false;
            this.dgvGRNList.AllowUserToDeleteRows = false;
            this.dgvGRNList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGRNList.Location = new System.Drawing.Point(30, 38);
            this.dgvGRNList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvGRNList.Name = "dgvGRNList";
            this.dgvGRNList.ReadOnly = true;
            this.dgvGRNList.RowHeadersWidth = 62;
            this.dgvGRNList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGRNList.Size = new System.Drawing.Size(1290, 246);
            this.dgvGRNList.TabIndex = 0;
            this.dgvGRNList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvGRNList_CellClick);
            // 
            // GRNForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1425, 1050);
            this.Controls.Add(this.groupBoxGRNList);
            this.Controls.Add(this.groupBoxActions);
            this.Controls.Add(this.groupBoxItems);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.lblRemarks);
            this.Controls.Add(this.dtpGRNDate);
            this.Controls.Add(this.lblGRNDate);
            this.Controls.Add(this.txtSupplier);
            this.Controls.Add(this.lblSupplier);
            this.Controls.Add(this.cmbPurchase);
            this.Controls.Add(this.lblPurchase);
            this.Controls.Add(this.picBarcode);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.lblBarcode);
            this.Controls.Add(this.txtGRNNo);
            this.Controls.Add(this.lblGRNNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "GRNForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GRN (Goods Receipt Note) Form";
            ((System.ComponentModel.ISupportInitialize)(this.picBarcode)).EndInit();
            this.groupBoxItems.ResumeLayout(false);
            this.groupBoxItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGRNItems)).EndInit();
            this.groupBoxActions.ResumeLayout(false);
            this.groupBoxGRNList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGRNList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblGRNNo;
        private System.Windows.Forms.TextBox txtGRNNo;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.PictureBox picBarcode;
        private System.Windows.Forms.Label lblPurchase;
        private System.Windows.Forms.ComboBox cmbPurchase;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.Label lblGRNDate;
        private System.Windows.Forms.DateTimePicker dtpGRNDate;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.GroupBox groupBoxItems;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.Label lblReceivedQty;
        private System.Windows.Forms.TextBox txtReceivedQty;
        private System.Windows.Forms.Label lblAcceptedQty;
        private System.Windows.Forms.TextBox txtAcceptedQty;
        private System.Windows.Forms.Label lblBatchNo;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.Label lblExpiryDate;
        private System.Windows.Forms.DateTimePicker dtpExpiryDate;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.DataGridView dgvGRNItems;
        private System.Windows.Forms.GroupBox groupBoxActions;
        private System.Windows.Forms.Button btnSaveDraft;
        private System.Windows.Forms.Button btnPost;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox groupBoxGRNList;
        private System.Windows.Forms.DataGridView dgvGRNList;
    }
}

