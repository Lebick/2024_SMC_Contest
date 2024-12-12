public static class KoreanPostposition
{
    // ¹ÞÄ§ À¯¹«¸¦ ÆÇº°
    public static bool HasJongseong(char ch)
    {
        // ÇÑ±Û À¯´ÏÄÚµå: °¡(0xAC00) ~ ÆR(0xD7A3)
        if (ch < 0xAC00 || ch > 0xD7A3)
            return false;

        // À¯´ÏÄÚµå °è»ê
        int code = ch - 0xAC00;
        int jongseong = code % 28; // Á¾¼º ÀÎµ¦½º (0ÀÌ¸é ¹ÞÄ§ ¾øÀ½)

        return jongseong != 0;
    }

    // Á¶»ç¸¦ ¹ÝÈ¯
    public static string GetPostposition(string word, string withJongseong, string withoutJongseong)
    {
        if (string.IsNullOrEmpty(word))
            return withoutJongseong;

        char lastChar = word[word.Length - 1];
        return HasJongseong(lastChar) ? withJongseong : withoutJongseong;
    }
}