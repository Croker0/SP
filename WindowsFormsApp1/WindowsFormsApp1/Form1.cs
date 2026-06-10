using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            // Проверяем, не пустое ли поле
            if (string.IsNullOrWhiteSpace(txtInput.Text))
            {
                MessageBox.Show("Введите текст для копирования!", "Внимание");
                return;
            }

            // Метод из методички: Clipboard.SetText
            Clipboard.SetText(txtInput.Text, TextDataFormat.Text);

            MessageBox.Show("Текст успешно скопирован в буфер обмена!", "Успех");
        }

        // 2. Кнопка: Вывести любой текст из буфера обмена
        private void btnPaste_Click(object sender, EventArgs e)
        {
            // Метод из методички: Clipboard.GetDataObject()
            IDataObject clipboardData = Clipboard.GetDataObject();

            // Проверяем, является ли содержимое буфера строкой (текстом)
            if (clipboardData.GetDataPresent(DataFormats.Text))
            {
                // Извлекаем текст и выводим в поле
                txtOutput.Text = (String)clipboardData.GetData(DataFormats.Text);
            }
            else
            {
                // Если в буфере не текст (например, картинка или ничего)
                txtOutput.Text = "Формат данных не поддерживается или буфер пуст";
            }
        }

        // 3. Кнопка: Вставить текст из буфера обмена в сторонний файл
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Сначала получаем текст из буфера (используем метод GetText из методички)
            string data = Clipboard.GetText(TextDataFormat.Text);

            if (string.IsNullOrWhiteSpace(data))
            {
                MessageBox.Show("Буфер обмена пуст или не содержит текст!", "Внимание");
                return;
            }

            // Открываем диалог сохранения файла
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Записываем данные в выбранный файл
                File.WriteAllText(saveFileDialog1.FileName, data);

                txtOutput.Text = data; // Дублируем в поле для наглядности
                MessageBox.Show($"Текст успешно сохранен в файл:\n{saveFileDialog1.FileName}", "Успех");
            }
        }
    }
}