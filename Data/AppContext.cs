using AvtoTrans.Models;

namespace AvtoTrans.Data;

class AppContext : IDisposable
{
    private static readonly List<Request> _requests = [];
    private static readonly List<Mechanic> _mechanics = [new(new FIO("Никитин Михаил Георгиевич"))];

    public List<Request> Requests { get => _requests; }
    public List<Mechanic> Mechanics { get => _mechanics; }

    private bool _isDisposed = false;


    public AppContext()
    {
       
    }


    public void Add(Request request)
    {
        _requests.Add(request);
    }

 

    public void Dispose()
    {
        Dispose(disposing: true);

        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        // check if already disposed 
        if (!_isDisposed)
        {
            // set the bool value to true 
            _isDisposed = true;
        }
    }
}