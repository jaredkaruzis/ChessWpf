//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ChessWpf {
//    public static class BoardUtilities {

//        public static Board ParsePGN(string pgn) {
//            var board = new Board();

//            var index = pgn.Contains(']') ? pgn.LastIndexOf(']') : 0;
//            var startOfGame = pgn.Substring(index).Trim();
//            var tokens = startOfGame.Split(' ');
//            var moves = new List<string>();
//            for (int i = 0; i < tokens.Count(); i++) {
//                if (!tokens[i].Contains('.')) {
//                    moves.Add(tokens[i]);
//                }
//            }
//            foreach (var move in moves) {
//                board.SubmitMove(move);
//            }
//            return board;
//        }

//        public static Board ParseFEN(string fen) {

//            var board = new Board();

//            // create all square objects 
//            for (int x = 0; x < 8; x++) {
//                for (int y = 0; y < 8; y++) {
//                    Squares[x, y] = new Square(x, y);
//                }
//            }
//            var lines = fen.Split('/');

//            // first 8 lines are the board
//            for (int i = 0; i < 8; i++) {
//                var charArray = lines[i].ToCharArray();
//                var rowCounter = 0;
//                foreach (var c in charArray) {
//                    if (char.IsLetter(c)) {
//                        switch (c) {
//                            case 'p':
//                                Pieces.Add(new Pawn(Color.Black, Squares[rowCounter, i]));
//                                Squares[rowCounter, i].Piece.HasMoved = !(Squares[rowCounter, i].Piece.Y == 1);
//                                break;
//                            case 'P':
//                                Pieces.Add(new Pawn(Color.White, Squares[rowCounter, i]));
//                                Squares[rowCounter, i].Piece.HasMoved = !(Squares[rowCounter, i].Piece.Y == 6);
//                                break;
//                            case 'b':
//                                Pieces.Add(new Bishop(Color.Black, Squares[rowCounter, i]));
//                                break;
//                            case 'B':
//                                Pieces.Add(new Bishop(Color.White, Squares[rowCounter, i]));
//                                break;
//                            case 'n':
//                                Pieces.Add(new Knight(Color.Black, Squares[rowCounter, i]));
//                                break;
//                            case 'N':
//                                Pieces.Add(new Knight(Color.White, Squares[rowCounter, i]));
//                                break;
//                            case 'r':
//                                Pieces.Add(new Rook(Color.Black, Squares[rowCounter, i]));
//                                break;
//                            case 'R':
//                                Pieces.Add(new Rook(Color.White, Squares[rowCounter, i]));
//                                break;
//                            case 'q':
//                                Pieces.Add(new Queen(Color.Black, Squares[rowCounter, i]));
//                                break;
//                            case 'Q':
//                                Pieces.Add(new Queen(Color.White, Squares[rowCounter, i]));
//                                break;
//                            case 'k':
//                                Pieces.Add(new King(Color.Black, Squares[rowCounter, i]));
//                                break;
//                            case 'K':
//                                Pieces.Add(new King(Color.White, Squares[rowCounter, i]));
//                                break;

//                        }
//                        rowCounter++;
//                        if (rowCounter > 7) {
//                            break;
//                        }
//                        continue;
//                    }
//                    if (char.IsDigit(c)) {
//                        var number = int.Parse(c.ToString()); //Fix
//                        rowCounter += number;
//                        if (rowCounter > 7) {
//                            break;
//                        }
//                        continue;
//                    }
//                    Console.WriteLine($"Error Loading FEN at coordinates {rowCounter}, {i}");
//                }
//            }
//            // Last line contains other board values: CurrentPlayer, CastlingRights, EnpassantSquares, HalfmovesToDraw, TotalFullMoves
//            var lastLine = lines[7].Split(' ');
//            var currentTurn = Color.NoColor;
//            for (int i = 0; i < lastLine.Length; i++) {
//                switch (i) {
//                    case (1):    // PLAYER TURN
//                        if (lastLine[i].Contains("w")) {
//                            currentTurn = Color.White;
//                        }
//                        else if (lastLine[i].Contains("b")) {
//                            currentTurn = Color.Black;
//                        }
//                        break;
//                    case (2):   // CASTLING RIGHTS
//                        if (lastLine[i].Contains("-")) {
//                            foreach (var piece in Pieces) {
//                                if (piece.IsKing) {
//                                    piece.HasMoved = true;
//                                }
//                            }
//                        }
//                        else {
//                            // Black Queenside
//                            if (lastLine[i].Contains("q")) {
//                                Squares[0, 0].Piece.HasMoved = false;
//                                Squares[4, 0].Piece.HasMoved = false;
//                            }
//                            else if (Squares[0, 0].HasPiece && Squares[0, 0].Piece.IsRook) {
//                                Squares[0, 0].Piece.HasMoved = true;
//                            }
//                            // White Queenside
//                            if (lastLine[i].Contains("Q")) {
//                                Squares[0, 7].Piece.HasMoved = false;
//                                Squares[4, 7].Piece.HasMoved = false;
//                            }
//                            else if (Squares[0, 7].HasPiece && Squares[0, 7].Piece.IsRook) {
//                                Squares[0, 7].Piece.HasMoved = true;
//                            }
//                            // Black Kingside
//                            if (lastLine[i].Contains("k")) {
//                                Squares[7, 0].Piece.HasMoved = false;
//                                Squares[4, 0].Piece.HasMoved = false;
//                            }
//                            else if (Squares[7, 0].HasPiece && Squares[7, 0].Piece.IsRook) {
//                                Squares[7, 0].Piece.HasMoved = true;
//                            }
//                            // White Kingside
//                            if (lastLine[i].Contains("K")) {
//                                Squares[7, 7].Piece.HasMoved = false;
//                                Squares[4, 7].Piece.HasMoved = false;
//                            }
//                            else if (Squares[7, 7].HasPiece && Squares[7, 7].Piece.IsRook) {
//                                Squares[7, 7].Piece.HasMoved = true;
//                            }
//                        }
//                        break;
//                    case (3):   // ENPASSANT SQUARE
//                        var coords = lastLine[i].ToCharArray();
//                        if (coords[0] == '-') {
//                            break;
//                        }
//                        else {
//                            var x = _notationDictionary[coords[0]];
//                            var y = _notationDictionary[coords[1]];
//                            EnpassantSquare = Squares[x, y];
//                            EnpassantSquare.EnpassantFlag = true;
//                            if (y == 2) {
//                                EnpassantPawn = Squares[x, 3].Piece;
//                            }
//                            else if (y == 5) {
//                                EnpassantPawn = Squares[x, 4].Piece;
//                            }
//                        }
//                        break;
//                    case (4):   // TODO: HALFMOVE COUNT at this point dont bother
//                        FiftyMoveCounter = int.Parse(lastLine[i]);
//                        break;
//                    case (5):   // TURN COUNT IN FULL MOVES
//                        var turnCount = int.Parse(lastLine[i]);
//                        MoveCount = ((turnCount - 1) * 2) + (currentTurn == Color.White ? 0 : 1);
//                        break;

//                }
//            }
//        }

//    }
//}
