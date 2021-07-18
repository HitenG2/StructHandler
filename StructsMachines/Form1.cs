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
namespace StructsMachines
{
    
    struct Auto
    {
        public string number;
        public string brand;
        public int year;
        public string color;

        public string first_name;
        public string second_name;
        public string patronymic;

        public string city;
        public string street;
        public string house;
        public string apartment;

        public string t_inspection;

        public int identificator;
    }

    public partial class Form1 : Form
    {
        private string path = @"machines.dat";
        private List<Auto> machines = new List<Auto>();
        private List<Auto> temp = new List<Auto>();
        private List<Auto> lastAutos = new List<Auto>();
        private List<string> splitterWords = new List<string>();
        private List<string> currentWord = new List<string>();

        private string number;
        private string brand;
        private int year;
        private string color;
        private string first_name;
        private string second_name;
        private string patronymic;
        private string city;
        private string street;
        private string house;
        private string apartment;
        private string t_inspection;
        private bool tempt_inspection;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            addItems();
            dataGridView1_adress.Rows.Add();
            File_Read();
        }

        private void addItems()
        {
            for (int i = 1960; i <= 2020; ++i)
                comboBox2.Items.Add(i);
            for (int i = 0; i < comboBox1.Items.Count; ++i)
                ColumnBrand.Items.Add(comboBox1.Items[i]);
            for (int i = 0; i < listBox1.Items.Count; ++i)
                ColumnColor.Items.Add(listBox1.Items[i]);
            for (int i = 0; i < comboBox1.Items.Count; ++i)
                ColumnF3_brand.Items.Add(comboBox1.Items[i]);
            for (int i = 0; i < listBox1.Items.Count; ++i)
                ColumnF3_color.Items.Add(listBox1.Items[i]);
            for (int i = 0; i < comboBox1.Items.Count; ++i)
                comboBoxF3ProBrand.Items.Add(comboBox1.Items[i]);
            for (int i = 0; i < listBox1.Items.Count; ++i)
                comboBoxF3ProColor.Items.Add(listBox1.Items[i]);
        }
        private void toolStripButton1_MouseDown(object sender, MouseEventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            if (e.Button == MouseButtons.Left)
            {
                if (index - 1 == -1) return;
                else dataGridView1.CurrentCell = dataGridView1[0, --index];
            }
            else if (e.Button == MouseButtons.Right)
            {
                dataGridView1.CurrentCell = dataGridView1[0, 0];
            }
        }

        private void toolStripButton2_MouseDown(object sender, MouseEventArgs e) //навигация по списку
        {
            int index = dataGridView1.CurrentRow.Index;
            if (e.Button == MouseButtons.Left)
            {
                if (index + 1 == dataGridView1.Rows.Count) return;
                else dataGridView1.CurrentCell = dataGridView1[0, ++index];
            }
            else if (e.Button == MouseButtons.Right)
            {
                dataGridView1.CurrentCell = dataGridView1[0, dataGridView1.Rows.Count - 1];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == " " || comboBox1.Text == " " || comboBox2.Text == " " || listBox1.Text == " " ||
                textBox_firstname.Text == " " || textBox_secondname.Text == " " || textBox_middlename.Text == " " ||
                dataGridView1_adress[0, 0].Value == null || dataGridView1_adress[1, 0].Value == null ||
                dataGridView1_adress[2, 0] == null || dataGridView1_adress[3, 0].Value == null ||
                checkBox1.Checked == false && checkBox2.Checked == false)
            {
                MessageBox.Show("Введены не все поля", "Error", MessageBoxButtons.OK);
                return;
            }
            else
            {
                /*number = textBox1.Text;
                brand = comboBox1.Text;
                year = Convert.ToInt32(comboBox2.Text);
                color = listBox1.Text;
                first_name = textBox_firstname.Text;
                second_name = textBox_secondname.Text;
                patronymic = textBox_middlename.Text;
                city = dataGridView1_adress[0, 0].Value.ToString();
                street = dataGridView1_adress[1, 0].Value.ToString();
                house = dataGridView1_adress[2, 0].Value.ToString();
                apartment = dataGridView1_adress[3, 0].Value.ToString();*/
                if (checkBox1.Checked == true) t_inspection = "Просрочен";
                else t_inspection = "Не просрочен";
                temp.Add(new Auto()
                {
                    number = textBox1.Text,
                    brand = comboBox1.Text,
                    year = Convert.ToInt32(comboBox2.Text),
                    color = listBox1.Text,
                    first_name = textBox_firstname.Text,
                    second_name = textBox_secondname.Text,
                    patronymic = textBox_middlename.Text,
                    city = dataGridView1_adress[0, 0].Value.ToString(),
                    street = dataGridView1_adress[1, 0].Value.ToString(),
                    house = dataGridView1_adress[2, 0].Value.ToString(),
                    apartment = dataGridView1_adress[3, 0].Value.ToString(),
                    t_inspection = t_inspection
                });
                if (dataGridView1.RowCount == 0) button4.Visible = true;
                else
                {
                    button4.Visible = true;
                    button5.Visible = true;
                    button6.Visible = true;
                }
                tabControl1.SelectedIndex = 0;
            }
        }

        private void checkBox1_MouseClick(object sender, MouseEventArgs e)
        {
            checkBox2.Checked = false;
        }

        private void checkBox2_MouseClick(object sender, MouseEventArgs e)
        {
            checkBox1.Checked = false;
        }

        private void Addnote(int index)
        {
            string fullname = temp[temp.Count - 1].first_name + " " + temp[temp.Count - 1].second_name + " " + temp[temp.Count - 1].patronymic;
            string fulladress = temp[temp.Count - 1].city + " " + temp[temp.Count - 1].street + " " + temp[temp.Count - 1].house + " " + temp[temp.Count - 1].apartment;
            dataGridView1[0, index].Value = temp[temp.Count - 1].number;
            dataGridView1[1, index].Value = temp[temp.Count - 1].brand;
            dataGridView1[2, index].Value = temp[temp.Count - 1].color;
            dataGridView1[3, index].Value = temp[temp.Count - 1].year;
            dataGridView1[4, index].Value = fullname;
            dataGridView1[5, index].Value = fulladress;
            if (t_inspection == "Просрочен")
                dataGridView1[6, index].Value = true;
            else dataGridView1[6, index].Value = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
        }

        private void InsertAtEnd()
        {
            dataGridView1.Rows.Add();
            int index = dataGridView1.RowCount - 1;
            Addnote(index);
            machines.Add(new Auto { });
            machines[machines.Count - 1] = temp[0];
            temp.Clear();
        }

        private void InsertAfter()
        {
            if (dataGridView1.CurrentRow.Index + 1 == machines.Count) machines.Add(temp[0]);
            else machines.Insert(dataGridView1.CurrentRow.Index + 1, temp[0]);
            var temporary = machines[dataGridView1.CurrentRow.Index];
            if (dataGridView1.CurrentRow.Index + 1 == dataGridView1.RowCount) dataGridView1.Rows.Add();
            else dataGridView1.Rows.Insert(dataGridView1.CurrentRow.Index + 1, 1);
            int index = dataGridView1.CurrentRow.Index + 1;
            Addnote(index);
            temp.Clear();
        }

        private void InsertBefore()
        {
            machines.Add(temp[0]);
            var temporary = machines[dataGridView1.CurrentRow.Index];
            machines[dataGridView1.CurrentRow.Index] = temp[0];
            machines[dataGridView1.CurrentRow.Index + 1] = temporary;
            dataGridView1.Rows.Insert(dataGridView1.CurrentRow.Index, 1);
            int index = dataGridView1.CurrentRow.Index - 1;
            Addnote(index);
            temp.Clear();
        }

        private void buttonInsertAtEnd_Click(object sender, EventArgs e)
        {
            InsertAtEnd();
        }

        private void buttonInsertAfter_Click(object sender, EventArgs e)
        {
            InsertAfter();
        }

        private void buttonInsertBefore_Click(object sender, EventArgs e)
        {
            InsertBefore();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void File_Read()
        {
            machines.Clear();
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    for (int i = 0; reader.PeekChar() > -1; ++i)
                    {
                        /*number = reader.ReadString();
                        brand = reader.ReadString();
                        year = reader.ReadInt32();
                        color = reader.ReadString();

                        first_name = reader.ReadString();
                        second_name = reader.ReadString();
                        patronymic = reader.ReadString();

                        city = reader.ReadString();
                        street = reader.ReadString();
                        house = reader.ReadString();
                        apartment = reader.ReadString();

                        t_inspection = reader.ReadString();*/
                        machines.Add(new Auto()
                        {
                            number = reader.ReadString(),
                            brand = reader.ReadString(),
                            year = reader.ReadInt32(),
                            color = reader.ReadString(),
                            first_name = reader.ReadString(),
                            second_name = reader.ReadString(),
                            patronymic = reader.ReadString(),
                            city = reader.ReadString(),
                            street = reader.ReadString(),
                            house = reader.ReadString(),
                            apartment = reader.ReadString(),
                            t_inspection = reader.ReadString()
                        });
                    }
                }
            }
            catch
            {
                return;
            }
            finally
            {
                printlist(dataGridView1);
            }
        }

        private void ReadfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File_Read();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.Delete(path);
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                foreach (Auto s in machines)
                {
                    writer.Write(s.number);
                    writer.Write(s.brand);
                    writer.Write(s.year);
                    writer.Write(s.color);

                    writer.Write(s.first_name);
                    writer.Write(s.second_name);
                    writer.Write(s.patronymic);

                    writer.Write(s.city);
                    writer.Write(s.street);
                    writer.Write(s.house);
                    writer.Write(s.apartment);

                    writer.Write(s.t_inspection);
                }
            }
        }

        private void printlist(DataGridView dataGridView)
        {
            if (dataGridView.Rows.Count == 0)
            {
                for (int i = 0; i < machines.Count; ++i)
                {
                    string fullname = machines[i].first_name + " " + machines[i].second_name + " " + machines[i].patronymic;
                    string fulladress = machines[i].city + " " + machines[i].street + " " + machines[i].house + " " + machines[i].apartment;
                    if (machines[i].t_inspection == "Просрочен") tempt_inspection = true;
                    else tempt_inspection = false;
                    dataGridView.Rows.Add(machines[i].number, machines[i].brand, machines[i].color, machines[i].year, fullname, fulladress, tempt_inspection);
                }
            }
            else
            {
                dataGridView.Rows.Clear();
                printlist(dataGridView);
            }

        }

        private void вывестиСписокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printlist(dataGridView1);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var changedCell = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
            var temp1 = machines[e.RowIndex];
            switch (e.ColumnIndex)
            {
                case 0:
                    temp1.number = changedCell;
                    machines[e.RowIndex] = temp1;
                    break;
                case 1:
                    temp1.brand = changedCell;
                    machines[e.RowIndex] = temp1;
                    break;
                case 2:
                    temp1.color = changedCell;
                    machines[e.RowIndex] = temp1;
                    break;
                case 3:
                    temp1.year = int.Parse(changedCell);
                    machines[e.RowIndex] = temp1;
                    break;
                case 4:
                    string[] tempfullname = changedCell.Split(' ');
                    temp1.first_name = tempfullname[0];
                    temp1.second_name = tempfullname[1];
                    temp1.patronymic = tempfullname[2];
                    machines[e.RowIndex] = temp1;
                    break;
                case 5:
                    string[] tempfulladress = changedCell.Split(' ');
                    temp1.city = tempfulladress[0];
                    temp1.street = tempfulladress[1];
                    temp1.house = tempfulladress[2];
                    temp1.apartment = tempfulladress[3];
                    machines[e.RowIndex] = temp1;
                    break;
                case 6:
                    temp1.t_inspection = changedCell;
                    machines[e.RowIndex] = temp1;
                    break;
            }
        }

        private void updateCells(DataGridView dataGridView)
        {
            if (dataGridView.Rows.Count == 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    object[] items = new object[row.Cells.Count];
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        items[i] = row.Cells[i].Value;
                    }
                    dataGridView.Rows.Add(items);
                }
            }
            else
            {
                dataGridView2.Rows.Clear();
                updateCells(dataGridView2);
            }
        }

        private void button_update_form3_Click(object sender, EventArgs e)
        {
            updateCells(dataGridView2);
        }

        private void поНомеруАвтоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Sort(ColumnNumber, 0);
        }

        private void поФамилииВладельцаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Sort(ColumnFullName, 0);
        }

        private void сложнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int tempi = 0;
            for (int i = 0; i < machines.Count; ++i)
            {
                var thistemp = machines[i];
                thistemp.identificator = i;
                machines[i] = thistemp;
            }
            List<Auto> trueList = new List<Auto>();
            List<string> numbersAuto = new List<string>();
            trueList = machines;
            for (int i = 0; i < machines.Count; ++i)
            {
                string firstDel = trueList[i].number.Remove(0, 1);
                string numberWord = trueList[i].number.Remove(1, 3);
                numbersAuto.Add(firstDel.Remove(3, 2));
                var thistemp = trueList[i];
                thistemp.number = numberWord;
                trueList[i] = thistemp;
            }
            var sortedList = trueList.OrderBy(l => l.city).ThenBy(l => l.second_name).ThenBy(l => l.number);
            List<Auto> thisSortedList = sortedList.ToList();
            List<int> thisbuffer = new List<int>();
            for (int i = 0; i < trueList.Count; ++i)
            {
                thisbuffer.Add(thisSortedList[i].identificator);
            }
            for (int i = 0; i < trueList.Count; ++i)
            {
                int indexOfIntegerValue = thisbuffer.IndexOf(i);
                var thistemp = thisSortedList[indexOfIntegerValue];
                thistemp.number = thistemp.number.Insert(1, numbersAuto[i]);
                thisSortedList[indexOfIntegerValue] = thistemp;
            }
            tempi = 0;
            foreach (Auto sl in thisSortedList)
            {
                number = sl.number;
                brand = sl.brand;
                color = sl.color;
                year = sl.year;
                first_name = sl.first_name;
                second_name = sl.second_name;
                patronymic = sl.patronymic;
                city = sl.city;
                street = sl.street;
                house = sl.house;
                apartment = sl.apartment;
                t_inspection = sl.t_inspection;
                var tempmachines = machines[tempi];
                tempmachines.number = number;
                tempmachines.brand = brand;
                tempmachines.color = color;
                tempmachines.year = year;
                tempmachines.first_name = first_name;
                tempmachines.second_name = second_name;
                tempmachines.patronymic = patronymic;
                tempmachines.city = city;
                tempmachines.street = street;
                tempmachines.house = house;
                tempmachines.apartment = apartment;
                tempmachines.t_inspection = t_inspection;
                machines[tempi] = tempmachines;
                ++tempi;
            }
            printlist(dataGridView1);
        }

        private void deleteList()
        {
            MessageBox.Show("Вы действительно хотите удалить список?", "Предупреждение", MessageBoxButtons.YesNo);
            if (DialogResult == DialogResult.Yes)
            {
                File.Delete(path);
                dataGridView1.Rows.Clear();
                lastAutos = machines;
                machines.Clear();
            }
            else return;
        }

        private void удалитьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteList();
        }

        private void поискПоЗначениямПолейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            updateCells(dataGridView2);
        }

        private void отменитьУдалениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            machines = lastAutos;
            printlist(dataGridView1);
        }

        private void очиститьТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void boxes_Activate()
        {
            labelF3Field.Visible = true;
            comboBoxF3ChooseField.Visible = true;
            labelFieldforEnter.Visible = true;
            textBoxFieldforEnter.Visible = true;
        }

        private void comboBoxF3ModeSearch_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxF3ModeSearch.SelectedItem.ToString() == "Обычный")
            {
                updateCells(dataGridView2);
                conditionBoxesF3NormalMode();
                currentWord.Clear();
            }
            else
            {
                updateCells(dataGridView2);
                conditionBoxesF3ProMode();
            }
        }

        private void conditionBoxesF3ProMode()
        {
            dataGridView2.Location = new Point(3, 90);
            dataGridView2.Size = new Size(749, 248);
            labelFieldforEnter.Visible = false;
            textBoxFieldforEnter.Visible = false;
            comboBoxF3ChooseField.Visible = false;
            labelF3Field.Visible = false;
            groupBoxF3ProSets.Visible = true;
            buttonF3ProApplyFilter.Visible = true;
            buttonF3ProDiscard.Visible = true;
            checkBoxF3ProTinsWithout.Visible = true;
            checkBoxF3ProTinsWithout.Checked = true;
        }

        private void conditionBoxesF3NormalMode()
        {
            dataGridView2.Location = new Point(3, 46);
            dataGridView2.Size = new Size(749, 292);
            labelFieldforEnter.Visible = true;
            textBoxFieldforEnter.Visible = true;
            comboBoxF3ChooseField.Visible = true;
            labelF3Field.Visible = true;
            groupBoxF3ProSets.Visible = false;
            buttonF3ProApplyFilter.Visible = false;
            buttonF3ProDiscard.Visible = false;
            checkBoxF3ProTinsWithout.Visible = false;
        }

        enum column
        {
            number,
            brand,
            color,
            year,
            name,
            adress,
            tInsp
        }
        private void buttonF3ProApplyFilter_Click(object sender, EventArgs e)
        {
            updateCells(dataGridView2);
            currentWord.Clear();
            if (textBoxF3ProNumber.Text != null)
            {
                textDivider(textBoxF3ProNumber.Text);
                currentWord = new List<string>(splitterWords);
                splitterWords.Clear();
                filter(Convert.ToInt32(column.number));
            }
            if (comboBoxF3ProBrand.SelectedItem != null)
            {
                for (int r = 0; r < dataGridView2.Rows.Count; ++r)
                {
                    if (comboBoxF3ProBrand.SelectedItem.ToString() != dataGridView2[Convert.ToInt32(column.brand), r].Value.ToString())
                    {
                        dataGridView2.Rows.Remove(dataGridView2.Rows[r]);
                        --r;
                    }

                }
            }
            if (comboBoxF3ProColor.SelectedItem != null)
            {
                for (int r = 0; r < dataGridView2.Rows.Count; ++r)
                {
                    if (comboBoxF3ProColor.SelectedItem.ToString() != dataGridView2[Convert.ToInt32(column.color), r].Value.ToString())
                    {
                        dataGridView2.Rows.Remove(dataGridView2.Rows[r]);
                        --r;
                    }
                }
            }
            if (textBoxF3ProYear.Text != "")
            {
                for (int r = 0; r < dataGridView2.Rows.Count; ++r)
                {
                    if (textBoxF3ProYear.Text != dataGridView2[Convert.ToInt32(column.year), r].Value.ToString())
                    {
                        dataGridView2.Rows.Remove(dataGridView2.Rows[r]);
                        --r;
                    }
                }
            }
            if (textBoxF3ProName.Text != "")
            {
                textDivider(textBoxF3ProName.Text);
                currentWord = new List<string>(splitterWords);
                splitterWords.Clear();
                filter(Convert.ToInt32(column.name));
            }
            if (textBoxF3ProAdress.Text != "")
            {
                textDivider(textBoxF3ProAdress.Text);
                currentWord = new List<string>(splitterWords);
                splitterWords.Clear();
                filter(Convert.ToInt32(column.adress));
            }
            if (checkBoxF3ProTinsWithout.Checked == false)
            {
                for (int r = 0; r < dataGridView2.Rows.Count; ++r)
                {
                    if (checkBoxF3ProTIns.Checked != (bool)dataGridView2[Convert.ToInt32(column.tInsp), r].EditedFormattedValue)
                    {
                        dataGridView2.Rows.Remove(dataGridView2.Rows[r]);
                        --r;
                    }
                }
            }
        }

        private void buttonF3ProDiscard_Click(object sender, EventArgs e)
        {
            textBoxF3ProNumber.Text = null;
            comboBoxF3ProBrand.SelectedItem = null;
            comboBoxF3ProColor.SelectedItem = null;
            textBoxF3ProYear.Text = null;
            textBoxF3ProName.Text = null;
            textBoxF3ProAdress.Text = null;
            checkBoxF3ProTIns.Checked = false;
            updateCells(dataGridView2);
        }

        private void textBoxFieldforEnter_TextChanged(object sender, EventArgs e)
        {
            updateCells(dataGridView2);
            filter(comboBoxF3ChooseField.SelectedIndex);
        }

        private void comboBoxF3ChooseField_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxFieldforEnter.Enabled = true;
        }

        private void filter(int columnIndex)
        {
            for (int r = 0; r < dataGridView2.Rows.Count; ++r)
            {
                textDivider(dataGridView2[columnIndex, r].Value.ToString());
                int coincidences = 0;
                if (currentWord.Count <= splitterWords.Count)
                {
                    for (int i = 0; i < currentWord.Count; ++i)
                    {
                        if (currentWord[i] == splitterWords[i]) ++coincidences;
                    }
                }
                if (coincidences != currentWord.Count)
                {
                    dataGridView2.Rows.Remove(dataGridView2.Rows[r]);
                    --r;
                }
                splitterWords.Clear();
            }
        }

        private void textDivider(string text)
        {
            char[] thisword = text.ToCharArray();
            for (int i = 0; i < thisword.Length; ++i)
            {
                splitterWords.Add(thisword[i].ToString());
            }
        }

        private void textBoxFieldforEnter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back && textBoxFieldforEnter.Text == "") currentWord.Clear();
            else if (e.KeyChar == (char)Keys.Back && currentWord.Count != 0) currentWord.RemoveAt(currentWord.Count - 1);
            else currentWord.Add(e.KeyChar.ToString());
        }

        private void deleteCurrentRow(DataGridView dataGridView)
        {
            int index = dataGridView.CurrentRow.Index;
            dataGridView.Rows.Remove(dataGridView.Rows[index]);
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteCurrentRow(dataGridView1);
        }

        private void toolStripButton3_Click(object sender, EventArgs e) => File_Read();

        private void вКонецToolStripMenuItem_Click(object sender, EventArgs e) => InsertAtEnd();

        private void доВыделеннойСтрокиToolStripMenuItem_Click(object sender, EventArgs e) => InsertBefore();

        private void послеВыделеннойСтрокиToolStripMenuItem_Click(object sender, EventArgs e) => InsertAfter();

        private void фильтрацияToolStripMenuItem_Click(object sender, EventArgs e) => tabControl1.SelectedIndex = 2;

        private void поОдномуПолюToolStripMenuItem_Click(object sender, EventArgs e) => tabControl1.SelectedIndex = 2;

        private void toolStripButtonRemoveCurRow_Click(object sender, EventArgs e) => deleteCurrentRow(dataGridView1);

        private void toolStripAddNote_Click(object sender, EventArgs e) => tabControl1.SelectedIndex = 1;

        private void toolStripDeleteFile_Click(object sender, EventArgs e) => deleteList();
    }
}