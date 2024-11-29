using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace InventoryManagerMAUI.Interface
{
    public interface IDbCommands<T> where T : class
    {
        public ObservableCollection<T> Collection { get; set; }

        
        public ICommand DeleteCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand AddCommand { get; }
        
        
        void OnAdd(object obj);

        void LoadData();
        void OnUpdate(T status);

        void OnDelete(T status);
    } 
}
