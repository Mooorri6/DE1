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
    public partial class Form1 : Form
    {
        private chimDBEntities dbContext;
        public Form1(string username, string role)
        {
            InitializeComponent();

            dbContext = new chimDBEntities();

            LoadPartners();
            LoadHistoryData();
        }


        private void LoadPartners()
        {
            flowLayoutPanel1.Controls.Clear();

            var partners = dbContext.Partners.ToList();

            foreach (var partner in partners)
            {
                var partnerBlock = CreatePartnerBlock(partner);
                flowLayoutPanel1.Controls.Add(partnerBlock);
            }

        }

        private void LoadHistoryData()
        {
            try
            {
                var records = dbContext.PartnerServices
                    .Select(r => new
                    {
                        r.PartnerServiceID,
                        r.ServiceID,
                        r.PartnerID,
                        r.Количество_услуг,
                        r.Дата_выполнения
                    })
                    .ToList();

                dataGridView1.DataSource = records;
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки записей: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private Panel CreatePartnerBlock(Partners partner)
        {
            Panel panel = new Panel();
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Size = new Size(500, 120);
            panel.Margin = new Padding(10);
            panel.BackColor = Color.White;
            panel.Cursor = Cursors.Hand;

            panel.Click += (s, e) => ShowPartner(partner);

            Label lblTypeName = new Label();
            lblTypeName.Text = $"{partner.Тип_партнера} | {partner.Наименование_партнера}";
            lblTypeName.Font = new Font("Franklin Gothic Medium", 10F, FontStyle.Bold);
            lblTypeName.Location = new Point(10, 10);
            lblTypeName.Size = new Size(220, 20);

            Label lblDirector = new Label();
            lblDirector.Text = partner.Руководитель;
            lblDirector.Font = new Font("Franklin Gothic Medium", 9F, FontStyle.Regular);
            lblDirector.Location = new Point(10, 35);
            lblDirector.Size = new Size(220, 20);

            Label lblPhone = new Label();
            lblPhone.Text = partner.Телефон_партнера ?? "Телефон не указан";
            lblPhone.Font = new Font("Franklin Gothic Medium", 9F, FontStyle.Regular);
            lblPhone.Location = new Point(10, 60);
            lblPhone.Size = new Size(220, 20);

            Label lblRating = new Label();
            lblRating.Text = $"Рейтинг: {partner.Рейтинг}";
            lblRating.Font = new Font("Franklin Gothic Medium", 9F, FontStyle.Bold);
            lblRating.Location = new Point(10, 85);
            lblRating.Size = new Size(220, 20);

            panel.Controls.Add(lblTypeName);
            panel.Controls.Add(lblDirector);
            panel.Controls.Add(lblPhone);
            panel.Controls.Add(lblRating);

            return panel;
        }

        private void ShowPartner(Partners partner)
        {

            PartnerEditForm editForm = new PartnerEditForm(partner, dbContext);

            editForm.FormClosed += (s, e) => LoadPartners();
            editForm.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddPartnerForm editForm = new AddPartnerForm(dbContext);

            editForm.ShowDialog();
            LoadPartners();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string partnerIdText = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(partnerIdText))
            {
                MessageBox.Show("Введите ID партнера");
                return;
            }

            if (!int.TryParse(partnerIdText, out int partnerId))
            {
                MessageBox.Show("ID партнера это числом");
                return;
            }

            try
            {
                var partner = dbContext.Partners.FirstOrDefault(p => p.PartnerID == partnerId);

                if (partner == null)
                {
                    MessageBox.Show("Партнер не найден");
                    return;
                }

                var partnerHistory = dbContext.PartnerServices
                    .Where(ph => ph.PartnerID == partnerId)
                    .Select(ph => new
                    {
                        ph.PartnerServiceID,
                        ph.ServiceID,
                        ph.PartnerID,
                        ph.Количество_услуг,
                        ph.Дата_выполнения
                    })
                    .ToList();

                if (!partnerHistory.Any())
                {
                    MessageBox.Show($"Для партнера '{partner.Наименование_партнера}' не найдено истории");
                    dataGridView1.DataSource = null;
                    return;
                }

                dataGridView1.DataSource = partnerHistory;
                MessageBox.Show($"Найдено записей истории: {partnerHistory.Count} для партнера: {partner.Наименование_партнера}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadHistoryData();
        }
    }
}
