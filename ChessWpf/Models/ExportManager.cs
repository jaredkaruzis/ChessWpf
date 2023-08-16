namespace ChessWpf; 

public class ExportManager : IExportManager {

    private readonly IBoardModel _boardModelManager;

    public ExportManager(IBoardModel boardModel) { 
        _boardModelManager = boardModel;
    }
}
