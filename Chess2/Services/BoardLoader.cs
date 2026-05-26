using Chess2.Models;
using Chess2.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess2.Services
{
    /// <summary>
    /// Инструмент для загрузки доски
    /// </summary>
    public class BoardLoader
    {
        private Random _random = new();

        public readonly string[] Presets =
        {
            //Стартовая позиция
            "rnbqkbnr" +
            "pppppppp" +
            "........" +
            "........" +
            "........" +
            "........" +
            "PPPPPPPP" +
            "RNBQKBNR",
            //Конь в центре под давлением
            "r...k..r" +
            "pppq.ppp" +
            "..n.b..." +
            "...p...." +
            "..PN...." +
            "...B...." +
            "PPQ..PPP" +
            "R...K..R",
            //Почти эндшпиль
            "........" +
            "...k...." +
            "..p....." +
            "....n..." +
            "...N...." +
            "..P....." +
            "....K..." +
            "........",
            //Сицилианская структура
            "r.bqk..r" +
            "pp..ppbp" +
            "..np.np." +
            "...p...." +
            "...NP..." +
            "..N....." +
            "PPP..PPP" +
            "R.BQKB.R",
            //Конь под атакой слона и ферзя
            "r...k..." +
            "pp..pppp" +
            "..b....." +
            "....q..." +
            "...N...." +
            "..P....." +
            "PP...PPP" +
            "R...K..R",
            //Итальянская партия
            "r.bqk.nr" +
            "pppp.ppp" +
            "..n....." +
            "..b.N..." +
            "...P...." +
            ".....N.." +
            "PPP..PPP" +
            "R.BQK..R",
            //Сложная позиция - топчик для демонстрации
            "r...k..r" +
            "pp..pppp" +
            "..n....." +
            "...b...." +
            "...Nq..." +
            "...P...." +
            "PPP..PPP" +
            "R...K..R"
        };

        public void LoadRandomBoard(MainViewModel viewModel)
        {
            string preset = Presets[
                _random.Next(Presets.Length)];

            for (int i = 0; i < 64; i++)
            {
                char c = preset[i];

                var cell = viewModel.Cells[i];

                cell.Piece = PieceFactory.Create(c);

                cell.PieceSymbol =
                    cell.Piece?.Symbol ?? "";
            }
        }
    }
}
