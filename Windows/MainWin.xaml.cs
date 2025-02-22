using System.Collections.ObjectModel;
using System.Windows;
using AvtoTrans.Models;

namespace AvtoTrans.Windows;

public partial class MainWin : Window
{

    public ObservableCollection<Request>? Requests { get; set; } = new();
    public Request? SelectRequest { get; set; }



    public MainWin()
    {
        InitializeComponent();
        DataContext = this;
    }


    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        using (Data.AppContext appContext = new())
        {
            Requests = new ObservableCollection<Request>(appContext.Requests);
        }
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (SelectRequest == null)
        {
            MessageBox.Show("Выберете заявку");
            return;
        }
        EditRequestWin EditWin = new(SelectRequest);
        EditWin.ShowDialog();
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        AddRequestWin addWin = new();
        addWin.ShowDialog();
    }
}