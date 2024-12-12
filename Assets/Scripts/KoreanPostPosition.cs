public static class KoreanPostposition
{
    // ��ħ ������ �Ǻ�
    public static bool HasJongseong(char ch)
    {
        // �ѱ� �����ڵ�: ��(0xAC00) ~ �R(0xD7A3)
        if (ch < 0xAC00 || ch > 0xD7A3)
            return false;

        // �����ڵ� ���
        int code = ch - 0xAC00;
        int jongseong = code % 28; // ���� �ε��� (0�̸� ��ħ ����)

        return jongseong != 0;
    }

    // ���縦 ��ȯ
    public static string GetPostposition(string word, string withJongseong, string withoutJongseong)
    {
        if (string.IsNullOrEmpty(word))
            return withoutJongseong;

        char lastChar = word[word.Length - 1];
        return HasJongseong(lastChar) ? withJongseong : withoutJongseong;
    }
}