using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DE1
{
    public partial class PartnerEditForm : Form
    {
        private Partners _partner;
        private chimDBEntities _dbContext;

        public PartnerEditForm(Partners partner, chimDBEntities dbContext)
        {
            InitializeComponent();

            _partner = partner;
            _dbContext = dbContext;

            LoadPartnerData();
        }

        private void LoadPartnerData()
        {
            textBox1.Text = _partner.Тип_партнера;
            textBox2.Text = _partner.Наименование_партнера;
            textBox3.Text = _partner.Руководитель;
            textBox4.Text = _partner.Электронная_почта_партнера;
            textBox5.Text = _partner.Телефон_партнера;
            textBox8.Text = _partner.Юридический_адрес_партнера;
            textBox7.Text = _partner.ИНН;
            textBox6.Text = _partner.Рейтинг?.ToString();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                _partner.Тип_партнера = textBox1.Text;
                _partner.Наименование_партнера = textBox2.Text;
                _partner.Руководитель = textBox3.Text;
                _partner.Электронная_почта_партнера = textBox4.Text;
                _partner.Телефон_партнера = textBox5.Text;
                _partner.Юридический_адрес_партнера = textBox8.Text;
                _partner.ИНН = textBox7.Text;

                if (int.TryParse(textBox6.Text, out int rating))
                {
                    _partner.Рейтинг = rating;
                }
                else
                {
                    _partner.Рейтинг = null; // если рейтинг не число
                }

                _dbContext.SaveChanges();

                MessageBox.Show("Данные успешно сохранены!", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Кнопка отмены
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
