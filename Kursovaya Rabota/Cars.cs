using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Kursovaya_Rabota
{
    public partial class Cars : Form
    {
        MySqlConnection conn;
        private void Cars_Load(object sender, EventArgs e)
        {
            string connStr = "server=caseum.ru;port=33333;user=st_1_22_19;database=st_1_22_19;password=97035229;";
            conn = new MySqlConnection(connStr);
            
        }
        public Cars()
        {
            InitializeComponent();
        }
        public void GetListCars(ListBox lb)
        {
            //Чистим ListBox
            lb.Items.Clear();
            // устанавливаем соединение с БД
            conn.Open();
            // запрос
            string sql = $"SELECT * FROM t_Cars";
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, conn);
            // объект для чтения ответа сервера
            MySqlDataReader reader = command.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                // элементы массива [] - это значения столбцов из запроса SELECT
                lb.Items.Add($"Марка Автомобиля: {reader[1].ToString()} Модель Автомобиля: {reader[2].ToString()} Номер Автомобиля: {reader[4].ToString()}");

            }
            reader.Close(); // закрываем reader
            // закрываем соединение с БД
            conn.Close();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
              
        public bool InsertCars(string Marka, string Model,string Number)
        {
            //определяем переменную, хранящую количество вставленных строк
            int InsertCount = 0;
            //Объявляем переменную храняющую результат операции
            bool result = false;
            // открываем соединение
            conn.Open();
            // запросы
            // запрос вставки данных
            string query = $"INSERT INTO t_Marka (titleMarks) VALUES ('{Marka}')";
            string query1 = $"INSERT INTO t_Model (titleModel) VALUES ('{Model}')";
            string query2 = $"INSERT INTO t_Cars (NumberTS) VALUES ('{Number}')";
            try
            {
                // объект для выполнения SQL-запроса
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlCommand command1 = new MySqlCommand(query1, conn);
                MySqlCommand command2 = new MySqlCommand(query2, conn);
                // выполняем запрос
                InsertCount = command.ExecuteNonQuery();
                InsertCount = command1.ExecuteNonQuery();
                InsertCount = command2.ExecuteNonQuery();
                // закрываем подключение к БД
            }
            catch
            {
                //Если возникла ошибка, то запрос не вставит ни одной строки
                InsertCount = 0;
            }
            finally
            {
                //Но в любом случае, нужно закрыть соединение
                conn.Close();
                //Ессли количество вставленных строк было не 0, то есть вставлена хотя бы 1 строка
                if (InsertCount != 0)
                {
                    //то результат операции - истина
                    result = true;
                }
            }
            //Вернём результат операции, где его обработает алгоритм
            return result;
        }
        //Объявляем соединения с БД
        public bool DeleteCars(string idCar,string idMarka,string idModel)
        {
            //определяем переменную, хранящую количество вставленных строк
            int InsertCount = 0;
            //Объявляем переменную храняющую результат операции
            bool result = false;
            // открываем соединение
            conn.Open();
            // запрос удаления данных
            string query = $"DELETE FROM t_Cars WHERE (idCar='{idCar}')";
            string query2 = $"DELETE FROM t_Marka WHERE (idMarka='{idMarka}')";
            string query3 = $"DELETE FROM t_Model WHERE (idModel='{idModel}')";
            try
            {
                // объект для выполнения SQL-запроса
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlCommand command1 = new MySqlCommand(query2,conn);
                MySqlCommand command2 = new MySqlCommand(query3 ,conn);
                // выполняем запрос
                InsertCount = command.ExecuteNonQuery();
                InsertCount= command1.ExecuteNonQuery();    
                InsertCount= command2.ExecuteNonQuery();
                // закрываем подключение к БД
            }
            catch
            {
                //Если возникла ошибка, то запрос не вставит ни одной строки
                InsertCount = 0;
            }
            finally
            {
                //Но в любом случае, нужно закрыть соединение
                conn.Close();
                //Ессли количество вставленных строк было не 0, то есть вставлена хотя бы 1 строка
                if (InsertCount != 0)
                {
                    //то результат операции - истина
                    result = true;
                }
            }
            //Вернём результат операции, где его обработает алгоритм
            return result;
        }
        //КНОПКА ДОБАВЛЕНИЯ
        private void button1_Click(object sender, EventArgs e)
        {
            //Объявляем переменные для вставки в БД
            string Marka = textBox1.Text;
            string Model = textBox2.Text;
            string Number = textBox3.Text;
            //Если метод вставки записи в БД вернёт истину, то просто обновим список и увидим вставленное значение
            if (InsertCars(Marka, Model,Number))
            {
                GetListCars(Автомобили);
            }
            //Иначе произошла какая то ошибка и покажем пользователю уведомление
            else
            {
                MessageBox.Show("Произошла ошибка.", "Ошибка");
            }
          
        }
        //КНОПКА УДАЛЕНИЯ
        private void button2_Click(object sender, EventArgs e)
        {
            string idCar = textBox4.Text;
            string idMarka = textBox4.Text;
            string idModel = textBox4.Text;
            //Если функция удалила строку, то
            if (DeleteCars(idCar,idMarka,idModel))
            {
                //Вызываем метод обновления листбокса
                GetListCars(Автомобили);
            }
            //Иначе произошла какая то ошибка и покажем пользователю уведомление
            else
            {
                MessageBox.Show("Произошла ошибка.", "Ошибка");
            }
        }
        public void SelectCar(ListBox lb)
        {
            lb.Items.Clear();
            string selected_id = textBox5.Text;
            string selected_id1 = textBox5.Text;
            string selected_id2 = textBox5.Text;
            // устанавливаем соединение с БД
            conn.Open();
            // запрос
            string sql = $"SELECT  titleMarks FROM t_Marka WHERE idMarka={selected_id}";
            string sql1 = $"SELECT titleModel FROM t_Model WHERE idModel={selected_id1}";
            string sql2 = $"SELECT NumberTS FROM t_Cars WHERE idCar={selected_id2}";
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlCommand command1 = new MySqlCommand(sql1, conn);
            MySqlCommand command2 = new MySqlCommand(sql2, conn);
            // объект для чтения ответа сервера
            MySqlDataReader reader = command.ExecuteReader();
            MySqlDataReader reader1 = command1.ExecuteReader();
            MySqlDataReader reader2 = command2.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                // элементы массива [] - это значения столбцов из запроса SELECT
                lb.Items.Add("Марка Автомобиля - " + reader[0].ToString());
                lb.Items.Add("Модель Автомобиля - " + reader1[1].ToString());
                lb.Items.Add($"Номер Автомобиля - " + reader2[2].ToString());
            }
            reader.Close(); // закрываем reader
            // закрываем соединение с БД
            conn.Close();
        }
        //КНОПКА ПОИСКА ПО ID 
        private void button3_Click(object sender, EventArgs e)
        {
            string selected_id = textBox5.Text;
            string selected_id1 = textBox5.Text;
            string selected_id2 = textBox5.Text;
            //Если метод вставки записи в БД вернёт истину, то просто обновим список и увидим вставленное значение
            if (InsertCars(selected_id, selected_id1, selected_id2))
            {
                SelectCar(Автомобили);
            }
            //Иначе произошла какая то ошибка и покажем пользователю уведомление
            else
            {
                MessageBox.Show("Произошла ошибка.", "Ошибка");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
