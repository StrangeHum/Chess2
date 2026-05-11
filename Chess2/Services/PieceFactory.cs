using Chess.Models;
using Chess2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess2.Services
{
    public static class PieceFactory
    {
        public static Piece? Create(char c)
        {
            return c switch
            {
                'P' => new Pawn { IsWhite = true },
                'R' => new Rook { IsWhite = true },
                'N' => new Knight { IsWhite = true },
                'B' => new Bishop { IsWhite = true },
                'Q' => new Queen { IsWhite = true },
                'K' => new King { IsWhite = true },

                'p' => new Pawn { IsWhite = false },
                'r' => new Rook { IsWhite = false },
                'n' => new Knight { IsWhite = false },
                'b' => new Bishop { IsWhite = false },
                'q' => new Queen { IsWhite = false },
                'k' => new King { IsWhite = false },

                _ => null
            };
        }
    }
}
