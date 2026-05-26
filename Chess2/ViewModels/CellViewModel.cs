using Chess2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Media;

namespace Chess2.ViewModels
{
    public class CellViewModel : INotifyPropertyChanged
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Piece? Piece { get; set; }

        private string _pieceSymbol = "";
        public string PieceSymbol
        {
            get => _pieceSymbol;
            set
            {
                _pieceSymbol = value;
                OnPropertyChanged(nameof(PieceSymbol));
            }
        }

        private Brush _background;
        public Brush Background
        {
            get => _background;
            set
            {
                _background = value;
                OnPropertyChanged(nameof(Background));
            }
        }

        private MarkerType _marker;
        public MarkerType Marker
        {
            get => _marker;
            set
            {
                _marker = value;
                OnPropertyChanged(nameof(Marker));
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
