public static class KoreanPostposition
{
    // 받침 유무를 판별
    public static bool HasJongseong(char ch)
    {
        // 한글 유니코드: 가(0xAC00) ~ 힣(0xD7A3)
        if (ch < 0xAC00 || ch > 0xD7A3)
            return false;

        // 유니코드 계산
        int code = ch - 0xAC00;
        int jongseong = code % 28; // 종성 인덱스 (0이면 받침 없음)

        return jongseong != 0;
    }

    // 조사를 반환
    public static string GetPostposition(string word, string withJongseong, string withoutJongseong)
    {
        if (string.IsNullOrEmpty(word))
            return withoutJongseong;

        char lastChar = word[word.Length - 1];
        return HasJongseong(lastChar) ? withJongseong : withoutJongseong;
    }
}