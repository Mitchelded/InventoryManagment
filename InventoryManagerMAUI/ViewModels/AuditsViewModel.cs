using System.Collections.ObjectModel;
using System.Windows.Input;
using InventoryManagerMAUI.Interface;
using InventoryManagment.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagerMAUI.ViewModels;

public class AuditsViewModel : ViewModelBase, IDbCommands<Audits>
{
    // private readonly InventoryManagmentEntities _db;
    public ObservableCollection<Audits> Collection { get; set; }
    
    public ICommand DeleteCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand AddCommand { get; }


    public AuditsViewModel()
    {
        // _db = new();
        LoadData();
    }


    public void OnAdd(object obj)
    {
        var status = new Audits()
        {
            AuditDate = _auditDate,
            PerformedByEmployeeID = _performedByEmployeeId,
            Notes = _notes,
            Discrepancies = _discrepancies,
            
        };
        Collection.Add(status);
        _db.SaveChanges();
    }

    public void OnUpdate(Audits status)
    {
        throw new NotImplementedException();
    }

    public void OnDelete(Audits status)
    {
        throw new NotImplementedException();
    }

    public void LoadData()
    {
        _db.Audits.Load();
        Collection = _db.Audits.Local.ToObservableCollection();
    }
    
    private int _idAudit;
    private DateTime _auditDate;
    private int _performedByEmployeeId;
    private string _notes;
    private string _discrepancies;

    public int IdAudit
    {
        get => _idAudit;
        set
        {
            if (value == _idAudit) return;
            _idAudit = value;
            OnPropertyChanged(nameof(IdAudit));
        }
    }

    public DateTime AuditDate
    {
        get => _auditDate;
        set
        {
            if (value.Equals(_auditDate)) return;
            _auditDate = value;
            OnPropertyChanged(nameof(AuditDate));
        }
    }

    public int PerformedByEmployeeId
    {
        get => _performedByEmployeeId;
        set
        {
            if (value == _performedByEmployeeId) return;
            _performedByEmployeeId = value;
            OnPropertyChanged(nameof(PerformedByEmployeeId));
        }
    }

    public string Notes
    {
        get => _notes;
        set
        {
            if (value == _notes) return;
            _notes = value;
            OnPropertyChanged(nameof(Notes));
        }
    }

    public string Discrepancies
    {
        get => _discrepancies;
        set
        {
            if (value == _discrepancies) return;
            _discrepancies = value;
            OnPropertyChanged(nameof(Discrepancies));
        }
    }
}