﻿namespace Demo
{
    partial class ProcessControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ProcessControl
            // 
            this.AllowDrop = true;
            this.AutoScroll = true;
            this.UseVisualStyleBackColor = true;
            this.TextChanged += new System.EventHandler(this.ProcessControl_TextChanged);
            this.SizeChanged += new System.EventHandler(this.ProcessControl_SizeChanged);
            this.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.ProcessControl_ControlAdded);
            this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.ProcessControl_ControlRemoved);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.TabProcess_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.TabProcess_DragEnter);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
