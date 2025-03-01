using System.Collections.ObjectModel;
using System.Windows;
using AvtoTrans.Enums;
using AvtoTrans.Models;

namespace AvtoTrans.Windows;

public partial class MainWin : Window
{
    private ObservableCollection<Request>? _requests;

    public ObservableCollection<Request>? Requests { get; set; }
    public Request? SelectRequest { get; set; }
    public uint? RequestId { get; set; }

    public RequestStatus? SelectStatus { get; set; }
    public IEnumerable<RequestStatus> Statuses { get; set; }


    public MainWin()
    {
        InitializeComponent();
        DataContext = this;
        _requests = new();
        Requests = new();

        using (Data.AppContext appContext = new())
        {
            foreach (var item in RequestInitializer.InitializeRequests(1000))
            {
                _requests.Add(item);
                Requests.Add(item);
                appContext.Add(item);
            }
        }

        Statuses = Enum.GetValues<RequestStatus>().Cast<RequestStatus>();
    }


    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        using (Data.AppContext appContext = new())
        {
            Requests.Clear();
            _requests.Clear();
            foreach (var item in new ObservableCollection<Request>(appContext.Requests))
            {
                _requests.Add(item);
                Requests.Add(item);
            }
        }
        lV1.Items.Refresh();
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

    private void SearchByID_Click(object sender, RoutedEventArgs e)
    {
        Requests.Clear();
        ObservableCollection<Request> requests = new(_requests.Where(r => r.Number == RequestId));
        if (requests.Count == 0)
            MessageBox.Show("Пусто");
        else
            Requests.Add(requests[0]);
    }

    private void SearchStatus_Click(object sender, RoutedEventArgs e)
    {
        Requests.Clear();
        ObservableCollection<Request> requests = new(_requests.Where(r => r.Status == SelectStatus));
        if (requests.Count == 0)
        {
            MessageBox.Show("Пусто");
        }
        else
        {
            foreach (var item in requests)
            {
                Requests.Add(item);
            }
        }
    }

    private void Statistics_Click(object sender, RoutedEventArgs e)
    {
        StatisticsWin win = new(_requests);
        win.ShowDialog();
    }
}