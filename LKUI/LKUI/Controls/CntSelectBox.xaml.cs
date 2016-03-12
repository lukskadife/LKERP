using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Collections;
using System.Linq;
using System.Windows.Media;

namespace LKUI.Controls
{
    /// <summary>
    /// Interaction logic for CntSelectBox.xaml
    /// </summary>
    public partial class CntSelectBox : UserControl
    {
        public CntSelectBox()
        {
            InitializeComponent();
            this.DataContextChanged += new DependencyPropertyChangedEventHandler(CntSelectBox_DataContextChanged);
        }

        void CntSelectBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            LoadCnt();
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(CntSelectBox),
          new FrameworkPropertyMetadata()
          {
              BindsTwoWayByDefault = true,
              DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
          });

        [System.ComponentModel.Bindable(true)]
        public IEnumerable ItemsSource 
        { 
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public bool ZorunluMu { get; set; }
        /// <summary>
        /// filtresi yapılacak kolon ismi
        /// </summary>
        public string FiltreAdi { get; set; } 
        /// <summary>
        /// Datagridde gözükecek kolonlar ve headerlar.Formatı: header1,bindEdilecekKolon1;header2,bindEdilecekKolon2
        /// </summary>
        public string Columns { get; set; }
        /// <summary>
        /// Seçilen kaydın labelda gösterilecek kolonu
        /// </summary>
        public string DisplayMember { get; set; }
        public string SelectedValuePath { get; set; }
        public object SelectedValue { get; set; }
        public string BindIdPath { get; set; }
        private object _SelectedItem;
        public object SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                //if (value != _SelectedItem) LoadCnt();
                _SelectedItem = value;
                if (value == null)
                {
                    SelectedValue = null;
                    TxtAranan.IsReadOnly = false;
                    TxtAranan.Text = "";
                }
            }
        }

        #region RoutedEvents
        public static readonly RoutedEvent SelectedItemChangedEvent = EventManager.RegisterRoutedEvent(
         "SelectedItemChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CntSelectBox));

        public event RoutedEventHandler SelectedItemChanged
        {
            add { AddHandler(SelectedItemChangedEvent, value); }
            remove { RemoveHandler(SelectedItemChangedEvent, value); }
        }

        public static readonly RoutedEvent ItemNotSelectEvent = EventManager.RegisterRoutedEvent(
 "ItemNotSelected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CntSelectBox));

        public event RoutedEventHandler ItemNotSelected
        {
            add { AddHandler(ItemNotSelectEvent, value); }
            remove { RemoveHandler(ItemNotSelectEvent, value); }
        }


        #endregion   

        private List<T> Ara<T>(IEnumerable ItemsSource) where T : class
        {
            List<T> list = new List<T>();
            if (string.IsNullOrEmpty(FiltreAdi)) return null;
            
            foreach (T item in ItemsSource)
            {
                if (item.GetType().GetProperty(FiltreAdi).GetValue(item, null).ToString().ToUpper().Contains(TxtAranan.Text.ToUpper()))
                    list.Add(item);
            }

            DGrid.SelectedIndex = 0;

            return list;
        }

        public bool GirisYapildiMi
        {
            get
            {
                if (this._SelectedItem == null && ZorunluMu)
                {
                    TxtAranan.BorderBrush = Brushes.Red;
                    return false;
                }
                else
                {
                    this.BorderBrush = TmpBrush;
                    return true;
                }
            }
        }

        private void TxtAranan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2) PopSonuclar.IsOpen = true;
            if (e.Key == Key.Escape) PopSonuclar.IsOpen = false;
            else if (TxtAranan.Text.Length >= 2) PopSonuclar.IsOpen = true;
        }

        private void TxtAranan_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PopSonuclar.IsOpen) DGrid.ItemsSource = Ara<dynamic>(ItemsSource);
        }

        private void LoadCnt()
        {
            try
            {
                if (this.DataContext != null)
                {
                    string value = this.DataContext.GetType().GetProperty(BindIdPath).GetValue(this.DataContext, null).ToString();
                    //if (value == SelectedValue.ToString()) return;
                    foreach (var item in ItemsSource)
                    {
                        if (item.GetType().GetProperty(SelectedValuePath).GetValue(item, null).ToString().Equals(value))
                        {
                            this.SelectedItem = item;
                            SelectedValue = item.GetType().GetProperty(SelectedValuePath).GetValue(item, null);
                            TxtAranan.IsReadOnly = true;
                            TxtAranan.Text = item.GetType().GetProperty(DisplayMember).GetValue(item, null).ToString();
                            break;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                this.SelectedItem = null;
                SelectedValue = null;
                TxtAranan.IsReadOnly = false;
                TxtAranan.Text = "";
            }
        }

        private Brush TmpBrush;

        private void UsrSelectBox_Loaded(object sender, RoutedEventArgs e)
        {
            TmpBrush = TxtAranan.BorderBrush;
            if (string.IsNullOrEmpty(Columns) == true) return;
            if (this.DGrid.Columns.Count > 0) return;

            string[] columns = Columns.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in columns)
            {
                string header = item.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
                string bind = item.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[1];

                DGrid.Columns.Add(new DataGridTextColumn() { Header = header, Binding = new Binding(bind) });
            }

            LoadCnt();
            DGrid.ItemsSource = this.ItemsSource;
        }

        private void DGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectedItem = DGrid.SelectedItem;
            if (DGrid.SelectedItem == null) SelectedValue = null;
            else
            {
                if (string.IsNullOrEmpty(SelectedValuePath) == false)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(SelectedValuePath) == false) SelectedValue = DGrid.SelectedItem.GetType().GetProperty(SelectedValuePath).GetValue(DGrid.SelectedItem, null);
                        if (string.IsNullOrEmpty(BindIdPath) == false && this.DataContext != null) this.DataContext.GetType().GetProperty(BindIdPath).SetValue(this.DataContext, SelectedValue, null);
                        if (string.IsNullOrEmpty(DisplayMember) == false)
                        {
                            TxtAranan.Text = DGrid.SelectedItem.GetType().GetProperty(DisplayMember).GetValue(DGrid.SelectedItem, null).ToString();
                            TxtAranan.IsReadOnly = true;
                        }
                        RaiseEvent(new RoutedEventArgs(SelectedItemChangedEvent));
                    }
                    catch (Exception exc)
                    {
                        this.SelectedItem = null;
                        SelectedValue = null;
                        TxtAranan.IsReadOnly = false;
                        TxtAranan.Text = "";
                    }
                }
            }

            PopSonuclar.IsOpen = false;
        }

        private void UsrSelectBox_Unloaded(object sender, RoutedEventArgs e)
        {
            PopSonuclar.IsOpen = false;
        }

        public void Clear()
        {
            TxtAranan.IsReadOnly = false;
            this.SelectedItem = null;
            this.SelectedValue = null;
            if (this.DataContext != null && this.BindIdPath != null) this.DataContext.GetType().GetProperty(BindIdPath).SetValue(this.DataContext, null, null);
            TxtAranan.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void PopSonuclar_Opened(object sender, EventArgs e)
        {
            DGrid.ItemsSource = Ara<dynamic>(ItemsSource);
            DGrid.SelectedItems.Clear();
        }
    }
}