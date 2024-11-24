
public enum ServerAction
{
    None,
    Init,           //초기화
    Invite,         //초대 대기중
    InviteCheck,    //초대 수락/거절
    InGame,         //게임으로
}

public enum GameResult
{
    None,
    MasterWin,
    SlaveWin,
    Draw,
}