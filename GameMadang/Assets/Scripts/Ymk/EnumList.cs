
public enum ServerAction
{
    None,
    Init,           //�ʱ�ȭ
    Invite,         //�ʴ� �����
    InviteCheck,    //�ʴ� ����/����
    InGame,         //��������
}

public enum GameResult
{
    None,
    MasterWin,
    SlaveWin,
    Draw,
}