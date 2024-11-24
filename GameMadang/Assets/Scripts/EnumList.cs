
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

public enum UnitTag
{
    //�׽�Ʈ�� ����
    TestUnit_1  = 0,
    TestUnit_2,

    //��������1 ����
    Stage1_1    = 100,
    Stage1_2,
    Stage1_3,
    Stage1_4,
    Stage1_5,
    Stage1_6,
    Stage1_7,
    Stage1_Other,

    //��������2 ����
    Stage2_1 = 200,
    Stage2_2,
    Stage2_3,
    Stage2_4,
    Stage2_5,
    Stage2_6,
    Stage2_7,
    Stage2_8,
    Stage2_9,
    Stage2_10,
    Stage2_11,
    Stage2_12,
    Stage2_Other,

    //��������3 ����
    Stage3_1 = 300,
    Stage3_2,
    Stage3_3,
    Stage3_4,
    Stage3_5,
    Stage3_6,
    Stage3_7,
    Stage3_8,
    Stage3_9,
    Stage3_10,
    Stage3_11,
    Stage3_12,
    Stage3_13,
    Stage3_Other,

    //��������4 ����
    Stage4_1 = 400,
    Stage4_2,
    Stage4_3,
    Stage4_4,
    Stage4_5,
    Stage4_6,
    Stage4_7,
    Stage4_8,
    Stage4_9,
    Stage4_Other,

    //��������5 ����
    Stage5_1 = 500,
    Stage5_2,
    Stage5_3,
    Stage5_4,
    Stage5_5,
    Stage5_6,
    Stage5_7,
    Stage5_8,
    Stage5_9,
    Stage5_10,
    Stage5_Other,
}

public enum Sound
{
    SE_WrongExplosion = 0,
    SE_LaserShooting,
    SE_GameEndPopup,
    SE_Explosion,
    SE_CutScene,
    SE_Click,

    BGM_DarkFight = 1000,
}