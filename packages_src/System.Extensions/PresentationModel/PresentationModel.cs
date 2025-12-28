using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace System.PresentationModel;

public enum ModelState
{
    Unchanged,
    New,
    Modified,
    Deleted
}

public class ViewModelCollection<TViewModel> : ObservableCollection<TViewModel>
    where TViewModel : ViewModel
{
    public ICommand SaveCommand { get; }

    public ICommand SaveAllCommand { get; }

    protected readonly IList<TViewModel> deletedItems;

    public ViewModelCollection()
    {
        SaveCommand = new Command(async () => await Save());

        SaveAllCommand = new Command(async () => await SaveAll());

        deletedItems = new List<TViewModel>();
    }

    public ViewModelCollection(IList<TViewModel> list)
        : base(list)
    {
        SaveCommand = new Command(async () => await Save());

        SaveAllCommand = new Command(async () => await SaveAll());

        deletedItems = new List<TViewModel>();
    }

    protected override void InsertItem(int index, TViewModel item)
    {
        OnAddNew(item);

        base.InsertItem(index, item);
    }

    protected virtual void OnAddNew(TViewModel viewModel)
    {
        viewModel.State = ModelState.New;
    }

    public virtual async Task Save()
    {
        await Task.CompletedTask;
    }

    public virtual async Task SaveAll()
    {
        deletedItems.Clear();

        await Task.CompletedTask;
    }

    protected override void RemoveItem(int index)
    {
        var item = this[index];

        OnRemoveItem(item);

        base.RemoveItem(index);
    }

    protected virtual void OnRemoveItem(TViewModel viewModel)
    {
        viewModel.State = ModelState.Deleted;

        deletedItems.Add(viewModel);
    }

    public IEnumerable<TViewModel> GetItemsBy(ModelState state)
    {
        IEnumerable<TViewModel> items;

        if (state == ModelState.Deleted)
        {
            items = deletedItems;
        }
        else
        {
            items = this.Where(p => p.State == state);
        }

        return items;
    }

    protected virtual void OnItemSaved(TViewModel viewModel)
    {
        viewModel.OnSave();
    }

    public delegate void StatusChangedHandler(string status);

    public event StatusChangedHandler StatusChanged;

    protected void SetStatus(string status)
    {
        if (StatusChanged != null)
        {
            StatusChanged(status);
        }

        //mainToolStripStatusLabel.Text = value;

        //statusBarTimer.Enabled = true;
    }
}

public abstract class ViewModel : ObservableObject, INotifyDataErrorInfo, IDataErrorInfo
{
    private ModelState state;
    public ModelState State
    {
        get { return state; }
        internal protected set
        {
            SetProperty(ref state, value);
        }
    }

    public override void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        base.OnPropertyChanged(propertyName);

        //

        if (State == ModelState.Unchanged && propertyName != "State" && propertyName != "OriginalVersion")
        {
            State = ModelState.Modified;
        }
    }

    public void SetAsModified()
    {
        State = ModelState.Modified;
    }

    public virtual void OnSave()
    {
        State = ModelState.Unchanged;
    }

    protected readonly Dictionary<string, IList<Exception>> validationErrors = new Dictionary<string, IList<Exception>>();

    protected void ClearErrors(string propertyName)
    {
        if (!validationErrors.ContainsKey(propertyName))
        {
            return;
        }

        validationErrors.Remove(propertyName);

        OnErrorsChanged(propertyName);
    }

    protected void RaiseErrorsChanged(string propertyName, Exception exception)
    {
        IList<Exception> errors;

        if (validationErrors.ContainsKey(propertyName))
        {
            errors = validationErrors[propertyName];
        }
        else
        {
            errors = new List<Exception>();

            validationErrors.Add(propertyName, errors);
        }

        errors.Add(exception);

        OnErrorsChanged(propertyName);
    }

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    protected virtual void OnErrorsChanged(string propertyName)
    {
        if (ErrorsChanged != null)
        {
            ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }

    public bool HasErrors
    {
        get { return !string.IsNullOrEmpty(Error) || validationErrors.Count > 0; }
    }

    public string Error { get; set; }

    public string this[string columnName]
    {
        get
        {
            if (validationErrors.Count == 0)
            {
                return null;
            }

            if (validationErrors[columnName].Count > 0)
            {
                return validationErrors[columnName][0].Message;
            }
            else
            {
                return null;
            }
        }
    }

    public IEnumerable GetErrors(string propertyName)
    {
        if (string.IsNullOrEmpty(propertyName) || !validationErrors.ContainsKey(propertyName))
        {
            return null;
        }

        return validationErrors[propertyName];
    }
}

public class Command : ICommand
{
    public event EventHandler CanExecuteChanged;

    private Action action;

    public Command(Action action)
    {
        this.action = action;
    }

    public bool CanExecute(object parameter)
    {
        throw new NotImplementedException();
    }

    public void Execute(object parameter)
    {
        action();
    }
}
