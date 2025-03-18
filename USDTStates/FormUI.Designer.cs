using System.Windows.Forms;

namespace USDTStates
{
    partial class FormUI
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.binanceLabel = new Label();
            this.bybitLabel = new Label();
            this.kucoinLabel = new Label();
            this.bitgetLabel = new Label();
            this.SuspendLayout();
            // 
            // binanceLabel
            // 
            this.binanceLabel.AutoSize = true;
            this.binanceLabel.Location = new System.Drawing.Point(81, 96);
            this.binanceLabel.Name = "label1";
            this.binanceLabel.Size = new System.Drawing.Size(37, 13);
            this.binanceLabel.TabIndex = 0;
            this.binanceLabel.Text = "USDT";
            // 
            // bybitLabel
            // 
            this.bybitLabel.AutoSize = true;
            this.bybitLabel.Location = new System.Drawing.Point(81, 136);
            this.bybitLabel.Name = "label2";
            this.bybitLabel.Size = new System.Drawing.Size(37, 13);
            this.bybitLabel.TabIndex = 1;
            this.bybitLabel.Text = "USDT";
            // 
            // kucoinLabel
            // 
            this.kucoinLabel.AutoSize = true;
            this.kucoinLabel.Location = new System.Drawing.Point(81, 174);
            this.kucoinLabel.Name = "label3";
            this.kucoinLabel.Size = new System.Drawing.Size(37, 13);
            this.kucoinLabel.TabIndex = 2;
            this.kucoinLabel.Text = "USDT";
            // 
            // bitgetLabel
            // 
            this.bitgetLabel.AutoSize = true;
            this.bitgetLabel.Location = new System.Drawing.Point(81, 212);
            this.bitgetLabel.Name = "label4";
            this.bitgetLabel.Size = new System.Drawing.Size(37, 13);
            this.bitgetLabel.TabIndex = 3;
            this.bitgetLabel.Text = "USDT";
            // 
            // FormUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.bitgetLabel);
            this.Controls.Add(this.kucoinLabel);
            this.Controls.Add(this.bybitLabel);
            this.Controls.Add(this.binanceLabel);
            this.Name = "FormUI";
            this.Text = "FormUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label binanceLabel;
        private Label bybitLabel;
        private Label kucoinLabel;
        private Label bitgetLabel;
    }
}

