using _01_Client.Models.DbSets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace _01_Client.Models.DataContext
{
    public class TasksData
    {
        SqlConnection connection_string;
        SqlCommand sql_command;
        SqlDataAdapter data_adapter;
        DataTable data_results;

        /// <summary>
        /// Конструктор с параметром ConnectionString "Строка Подключения"
        /// </summary>
        /// <param name="conn_string"></param>
        public TasksData(string conn_string)
        {
            connection_string = new SqlConnection(conn_string);
            sql_command = new SqlCommand();
            data_adapter = new SqlDataAdapter();
            data_results = new DataTable();

        }
        //Статья на хабре про мою мечту https://habrahabr.ru/post/144330/
        //// Var list = MappingData<User>(result)
        //public List<T> MapingData<T>(DataTable result) where T: new()
        //{
        //    T temp = new T();
        //    if (temp is User) {

        //    }

        //    return new List<object>();

        //}


        /// <summary>
        /// Возвращает строку Json
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public string ResultToJson(DataTable table)
        {
            StringBuilder sbuilder = new StringBuilder();

            sbuilder.Append("{\"Category");
            sbuilder.Append(table.TableName);
            sbuilder.Append("\":[");

            bool first = true;
            foreach (DataRow drow in table.Rows)
            {
                if (first)
                {
                    sbuilder.Append("{");
                    first = false;
                }
                else
                    sbuilder.Append(",{");

                bool firstColumn = true;
                foreach (DataColumn column in table.Columns)
                {
                    if (firstColumn)
                    {
                        sbuilder.Append(string.Format("\"{0}\":\"{1}\"", column.ColumnName, drow[column].ToString()));
                        firstColumn = false;
                    }
                    else
                        sbuilder.Append(string.Format(",\"{0}\":\"{1}\"", column.ColumnName, drow[column].ToString()));
                }
                sbuilder.Append("}");
            }

            sbuilder.Append("]}");

            return sbuilder.ToString();
        }
        /// <summary>
        /// Заполняет table переданным сюда result  
        /// </summary>
        /// <param name="result"></param>
        /// <param name="table"></param>
        public void FillTableAfterSelect(DataTable result, Table table)
        {
            TableHeaderRow thr = new TableHeaderRow();
            TableHeaderCell thc;
            foreach (DataColumn itemcell in result.Columns)
            {

                thc = new TableHeaderCell();
                thc.Text = itemcell.ColumnName;
                thr.Cells.Add(thc);

            }
            table.Rows.Add(thr);
            TableRow trow = new TableRow();
            TableCell tcell;
            foreach (DataRow item in result.Rows)
            {
                for (int i = 0; i <= (item.ItemArray.Count()) - 1; i++)
                {
                    tcell = new TableCell();
                    if (item[i].ToString() == "")
                    {
                        tcell.Text = "NULL";
                    }
                    else
                    {
                        tcell.Text = item[i].ToString();
                    }


                    trow.Cells.Add(tcell);
                }

                table.Rows.Add(trow);
                trow = new TableRow();
            }

        }
        /// <summary>
        /// Переобразует в string параметры для выборки аттрибутов базы
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public string SelectMany(params object[] attributes)
        {
            return ParamsMethod(attributes);
        }
        /// <summary>
        /// Переобразует в string параметры для сортировки аттрибутов базы
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public string OrderByMany(params object[] attributes)
        {
            return ParamsMethod(attributes);
        }
        /// <summary>
        /// Выбирает все аттрибуты из таблицы "tablename"
        /// </summary>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public DataTable SelectQuery(string tablename)
        {

            sql_command = new SqlCommand(("Select * from dbo." + tablename), connection_string);
            data_adapter = new SqlDataAdapter(sql_command);
            data_adapter.Fill(data_results);
            return data_results;
        }


        public DataTable SelectQuery(string tablename, string SelectManyMethodOrAttribute)
        {

            sql_command = new SqlCommand(("Select " + SelectManyMethodOrAttribute + " from dbo." + tablename), connection_string);
            data_adapter = new SqlDataAdapter(sql_command);
            data_adapter.Fill(data_results);
            return data_results;
        }
        public DataTable SelectQueryOrderBy(string tablename, string OrderByMethodOrAttribute)
        {
            sql_command = new SqlCommand(("Select * from dbo." + tablename + "Order by" + OrderByMethodOrAttribute), connection_string);
            data_adapter = new SqlDataAdapter(sql_command);
            data_adapter.Fill(data_results);
            return data_results;
        }
        public DataTable SelectQueryOrderBy(string tablename, string SelectManyMethodOrAttribute, string OrderByMethodOrAttribute)
        {
            sql_command = new SqlCommand(("Select " + SelectManyMethodOrAttribute + " from dbo." + tablename + "Order by" + OrderByMethodOrAttribute), connection_string);
            data_adapter = new SqlDataAdapter(sql_command);
            data_adapter.Fill(data_results);
            return data_results;
        }







        /////// HELP////////
        private static string ParamsMethod(object[] attributes)
        {
            string result = "";
            for (int i = 0; i < attributes.Length; i++)
            {
                if (i != attributes.Length - 1)
                {
                    result += attributes[i].ToString() + ",";
                }
                else
                {
                    result += attributes[i].ToString();
                }
            }
            return result;
        }

    }
}