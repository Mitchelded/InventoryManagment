using InventoryManagment.Models;

namespace InventoryManagerMAUI.ViewModels.ViewModel;

public class StudentsViewModel : ViewModelBase<Students>
{
    private int _idStudents;
    private string _firstName;
    private string _lastName;
    private string _patronymic;
    private string _studentIdСard;
    private int _departmentId;
    private string _course;

    public int IdStudents
    {
        get => _idStudents;
        set
        {
            if (value == _idStudents) return;
            _idStudents = value;
            OnPropertyChanged(nameof(IdStudents));
        }
    }

    public string FirstName
    {
        get => _firstName;
        set
        {
            if (value == _firstName) return;
            _firstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            if (value == _lastName) return;
            _lastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }

    public string Patronymic
    {
        get => _patronymic;
        set
        {
            if (value == _patronymic) return;
            _patronymic = value;
            OnPropertyChanged(nameof(Patronymic));
        }
    }

    public string StudentIDСard
    {
        get => _studentIdСard;
        set
        {
            if (value == _studentIdСard) return;
            _studentIdСard = value;
            OnPropertyChanged(nameof(StudentIDСard));
        }
    }

    public int DepartmentID
    {
        get => _departmentId;
        set
        {
            if (value == _departmentId) return;
            _departmentId = value;
            OnPropertyChanged(nameof(DepartmentID));
        }
    }

    public string Course
    {
        get => _course;
        set
        {
            if (value == _course) return;
            _course = value;
            OnPropertyChanged(nameof(Course));
        }
    }

    public override void OnAdd(object obj)
    {
        using InventoryManagmentEntities _db = new();
        var status = new Students()
        {
    IdStudents = _idStudents,
    FirstName = _firstName,
    LastName = _lastName,
    Patronymic = _patronymic,
    StudentIDСard = _studentIdСard,
    DepartmentID = _departmentId,
    Course = _course,
            
        };
        Collection.Add(status);
        _db.Add(status);
        _db.SaveChanges();
    }
}