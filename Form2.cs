using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_2
{
    public partial class Form2 : Form
    {
        private string currentFilePath = "";
        string currentUserPermission;

        public Form2(string currentUser, string userPermissions)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            currentUserLabel.Text = "User Name: " + currentUser;
            currentUserPermission = userPermissions;

            this.KeyPreview = true;
            this.Size = new Size(1200, 700);

            if (userPermissions.Equals("View"))
            {
                richTextBox.ReadOnly = true;
                this.Text = "Text Editor [View Only]";
            }
            else
            {
                richTextBox.ReadOnly = false;
                this.Text = "Text Editor";
            }

            for (int i = 0; i < 13; i++)
                fontSizeCB.Items.Add(i + 8);

            fontSizeCB.SelectedIndex = 0;
            fontSizeCB.DropDownStyle = ComboBoxStyle.DropDownList;
            richTextBox.SelectionFont = new Font(richTextBox.SelectionFont.FontFamily, 8);
            this.fontSizeCB.SelectedIndexChanged +=
                new System.EventHandler(fontSizeCB_SelectedIndexChanged);
        }

        private void ToolStripContainer1_ContentPanel_Load(object sender, EventArgs e) {  }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A simple text editor with more functionality than the standard included windows notepad text editor.\n\nThis text editor was created by: Zaid Arain\nVersion: 1.0", "About");
        }

        private void LogoutMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1();
            form.Show();
        }

        private void CutToolStrip_Click(object sender, EventArgs e)
        {
            richTextBox.Cut();
        }

        private void CopyToolStrip_Click(object sender, EventArgs e)
        {
            richTextBox.Copy();
        }

        private void PasteToolStrip_Click(object sender, EventArgs e)
        {
            richTextBox.Paste();
        }

        private void CutMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Cut();
        }

        private void CopyMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Copy();
        }

        private void PasteMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Paste();
        }

        private void NewMenuItem_Click(object sender, EventArgs e)
        {
            newFile();
        }

        private void OpenMenuItem_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void SaveMenuItem_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void SaveAsMenuItem_Click(object sender, EventArgs e)
        {
            saveAsFile();
        }

        private void NewToolStrip_Click(object sender, EventArgs e)
        {
            newFile();
        }

        private void OpenToolStrip_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void SaveToolStrip_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void SaveAsToolStrip_Click(object sender, EventArgs e)
        {
            saveAsFile();
        }

        private void BoldToolStrip_Click(object sender, EventArgs e)
        {
            if (richTextBox.SelectionFont != null)
            {
                Font currentFont = richTextBox.SelectionFont;
                FontStyle newFontStyle;

                if (richTextBox.SelectionFont.Bold == true)
                {
                    newFontStyle = FontStyle.Regular;
                    boldToolStrip.BackColor = SystemColors.Control;
                }
                else
                {
                    newFontStyle = FontStyle.Bold;
                    boldToolStrip.BackColor = SystemColors.MenuHighlight;
                    italicToolStrip.BackColor = SystemColors.Control;
                    underlineToolStrip.BackColor = SystemColors.Control;
                }

                richTextBox.SelectionFont = new Font(
                   currentFont.FontFamily,
                   currentFont.Size,
                   newFontStyle
                );
            }
        }

        private void ItalicToolStrip_Click(object sender, EventArgs e)
        {
            if (richTextBox.SelectionFont != null)
            {
                Font currentFont = richTextBox.SelectionFont;
                FontStyle newFontStyle;

                if (richTextBox.SelectionFont.Italic == true)
                {
                    newFontStyle = FontStyle.Regular;
                    italicToolStrip.BackColor = SystemColors.Control;
                }
                else
                {
                    newFontStyle = FontStyle.Italic;
                    italicToolStrip.BackColor = SystemColors.MenuHighlight;
                    boldToolStrip.BackColor = SystemColors.Control;
                    underlineToolStrip.BackColor = SystemColors.Control;
                }

                richTextBox.SelectionFont = new Font(
                   currentFont.FontFamily,
                   currentFont.Size,
                   newFontStyle
                );
            }
        }

        private void UnderlineToolStrip_Click(object sender, EventArgs e)
        {
            if (richTextBox.SelectionFont != null)
            {
                Font currentFont = richTextBox.SelectionFont;
                FontStyle newFontStyle;

                if (richTextBox.SelectionFont.Underline == true)
                {
                    newFontStyle = FontStyle.Regular;
                    underlineToolStrip.BackColor = SystemColors.Control;
                }
                else
                {
                    newFontStyle = FontStyle.Underline;
                    underlineToolStrip.BackColor = SystemColors.MenuHighlight;
                    boldToolStrip.BackColor = SystemColors.Control;
                    italicToolStrip.BackColor = SystemColors.Control;
                }

                richTextBox.SelectionFont = new Font(
                   currentFont.FontFamily,
                   currentFont.Size,
                   newFontStyle
                );
            }
        }

        private void fontSizeCB_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            richTextBox.SelectionFont = new Font(richTextBox.SelectionFont.FontFamily, Int32.Parse(fontSizeCB.SelectedItem.ToString()));
            richTextBox.Focus();
            boldToolStrip.BackColor = SystemColors.Control;
            italicToolStrip.BackColor = SystemColors.Control;
            underlineToolStrip.BackColor = SystemColors.Control;
        }

        private void newFile()
        {
            if (MessageBox.Show("Any unsaved progress will be lost. Continue?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                currentFilePath = "";
                richTextBox.Text = "";
            }
        }

        private void openFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = "Text files (*rtf; *.txt)|*rtf; *txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = openFileDialog.FileName;
                if (Path.GetExtension(openFileDialog.FileName).Equals(".rtf"))
                    richTextBox.LoadFile(openFileDialog.FileName);
                else
                    richTextBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void saveFile()
        {
            if (currentFilePath.Equals(""))
                saveAsFile();
            else
                if (Path.GetExtension(currentFilePath).Equals(".rtf"))
                richTextBox.SaveFile(currentFilePath);
            else
                richTextBox.SaveFile(currentFilePath, RichTextBoxStreamType.PlainText);
        }

        private void saveAsFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "RTF file|*.rtf|Text file|*.txt|All files (*.*)|*.*";
            saveFileDialog.Title = "Save a new text file";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog.FileName != "")
                {
                    if (Path.GetExtension(saveFileDialog.FileName).Equals(".rtf"))
                        richTextBox.SaveFile(saveFileDialog.FileName);
                    else
                        richTextBox.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                }
                else
                    MessageBox.Show("Please enter a valid file name");
            }
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
                newFile();
            if (e.Control && e.KeyCode == Keys.O)
                openFile();
            if (e.Control && e.KeyCode == Keys.S)
                saveFile();
            if (e.Control && e.KeyCode == Keys.X)
                richTextBox.Cut();
            if (e.Control && e.KeyCode == Keys.C)
                richTextBox.Copy();
            if (e.Control && e.KeyCode == Keys.V)
                richTextBox.Paste();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentUserPermission.Equals("Edit"))
                if (!currentFilePath.Equals("") || !richTextBox.Text.Equals(""))
                    if (MessageBox.Show("Any unsaved changes will be lost. Continue", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        Environment.Exit(0);
                    else
                        e.Cancel = true;
        }
    }
}
