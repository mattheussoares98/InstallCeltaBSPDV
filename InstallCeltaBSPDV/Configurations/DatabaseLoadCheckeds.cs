using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace InstallCeltaBSPDV.Configurations {
    internal static class DatabaseLoadCheckeds {
        //quando NÃO há o arquivo do banco de dados criado, o sistema o cria e insere os valores padrões "false" para cada "checkedBox". Quando altera um checkedBox, o sistema altera no banco de dados também
        //quando abre a aplicação, a aplicação faz a leitura das inforamções que estão no banco de dados e caso haja algum com o valor "true", já marca como "checked" o checkedbox correspondente

        private static readonly string pathOfData = Application.StartupPath + @"\dbSQLite.db"; //se quiser colocar o arquivo do banco de dados dentro de alguma outra pasta, primeiro precisa criar essa pasta caso ela não exista senão vai dar erro
        private static readonly string stringConnection = @"Data Source = " + pathOfData + "; Version = 3";

        public static void createTableAndInsertValues(EnableConfigurations enable) {
            SQLiteConnection connection = new SQLiteConnection(stringConnection);

            SQLiteCommand command = new SQLiteCommand();
            command.CommandText = "CREATE TABLE checkBoxs (";

            var controls = enable.flowLayoutPanelConfigurations.Controls;
            foreach(CheckBox checkBox in controls) {
                command.CommandText += $"{checkBox.Name} NVARCHAR(5),";
            }
            int indexToRemove = command.CommandText.LastIndexOf(","); //Obtendo o último índice
            command.CommandText = command.CommandText.Remove(indexToRemove); //removendo a última vírgula pra não dar erro no comando
            command.CommandText += ")";
            command.Connection = connection;

            try {
                connection.Open();
                command.ExecuteNonQuery();

                command.Dispose();

            } catch(Exception ex) {
                MessageBox.Show("Erro para criar o banco de dados que é usado para persistir os dados do que já foi realizado na instalação");
            } finally {
                connection.Close();
            }
            insertData(enable);
        }

        public static void selectAndUpdateCheckBoxValues(EnableConfigurations enable) {
            SQLiteConnection connection = new SQLiteConnection(stringConnection);
            try {

                connection.Open();

                string query = "select * from checkBoxs";

                DataTable data = new DataTable();

                SQLiteDataAdapter sqlCeDataAdapter = new SQLiteDataAdapter(query, stringConnection);

                sqlCeDataAdapter.Fill(data);


                foreach(DataRow row in data.Rows) {
                    foreach(var column in row.Table.Columns) {
                        var columnValue = row[$"{column}"];
                        var controls = enable.flowLayoutPanelConfigurations.Controls;
                        foreach(CheckBox checkBox in controls) {
                            if(column.ToString() == checkBox.Name.ToString() && columnValue.ToString() == "true") {
                                checkBox.Checked = true;
                            }
                        }
                    }
                }

            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            } finally {
                connection.Close();
            }
        }

        public static void insertData(EnableConfigurations enable) {
            //só vai inserir os dados quando o banco de dados não existir. Vai inserir todos com o valor inicial "false"
            SQLiteConnection connection = new SQLiteConnection(stringConnection);

            SQLiteCommand command = new SQLiteCommand();
            command.CommandText = $"INSERT INTO checkBoxs (";

            var controls = enable.flowLayoutPanelConfigurations.Controls;
            foreach(CheckBox checkBox in controls) {
                command.CommandText += $"{checkBox.Name},";
            }
            int indexToRemove = command.CommandText.LastIndexOf(","); //Obtendo o último índice
            command.CommandText = command.CommandText.Remove(indexToRemove); //removendo a última vírgula pra não dar erro no comando
            command.CommandText += ") VALUES (";
            foreach(CheckBox checkBox in controls) {
                command.CommandText += "'false',"; //mesmo não usando o checkBox.Name deixei fazendo o foreach pra pegar o tamanho certo de itens que precisa alterar
            }
            indexToRemove = command.CommandText.LastIndexOf(","); //Obtendo o último índice
            command.CommandText = command.CommandText.Remove(indexToRemove); //removendo a última vírgula pra não dar erro no comando

            command.CommandText += ");";
            command.Connection = connection;

            try {
                connection.Open();
                command.ExecuteNonQuery();

                command.Dispose();

            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            } finally {
                connection.Close();
            }
        }

        public static void updateData(CheckBox checkBox) {
            SQLiteConnection connection = new SQLiteConnection(stringConnection);
            String checkBoxValue;
            if(checkBox.Checked) {
                checkBoxValue = "true";
            } else {
                checkBoxValue = "false";
            }
            String checkBoxName = checkBox.Name;
            var command = connection.CreateCommand();
            try {

                connection.Open();

                command.CommandText = $"update checkBoxs set {checkBoxName} = '{checkBoxValue}'";
                int rowsAffected = command.ExecuteNonQuery();

            } catch(Exception ex) {
                MessageBox.Show(ex.Message);
            } finally {
                command.Dispose();
            }

        }
    }
}
