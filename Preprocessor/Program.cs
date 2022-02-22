using System.Text.RegularExpressions;
using static System.Linq.Enumerable;

void PrintBar() => Console.WriteLine(new string('=', 30));
string RefineName(string name) => name switch
{
    "ﾠ" => "나선우",
    _ => name
};

Console.OutputEncoding = System.Text.Encoding.UTF8;
PrintBar();
Console.WriteLine(@" 카카오톡 채팅 데이터 분석을 시작합니다.");
PrintBar();

Regex regex = new(@"^\[(.*?)\] \[\d*:\d* [AP]M\] (.+)$");
string[] lines = File.ReadAllLines(@"D:\Programming\.NET\C#\NochesLanguage\Preprocessor\sharetech.txt");

List<(string name, string msg)> chatData = new();
foreach (string line in lines)
{
    // 정상적인 문자열이 아니면 다음 문장으로
    if (string.IsNullOrWhiteSpace(line) || !regex.IsMatch(line))
        continue;

    // 샵검색, For public 으엥치놈
    if (new[] { "샵검색", "For", "public", "으엥치놈 : 수학 2" }.Any(line.Contains))
        continue;

    Match match = regex.Match(line);
    GroupCollection groups = match.Groups;

    string name = RefineName(groups[1].Value);
    string msg = groups[2].Value.Trim();

    // 의미 있는 메시지가 아니어도 다음 문장으로
    if (new[] { "Photo", "videos", "Emoticons", "This message was deleted." }.Any(x => x == msg))
        continue;

    if (msg.Contains(" photos"))
        continue;

    if (msg.Length <= 2)
        continue;

    chatData.Add((name, msg));
}

using StreamWriter file = new(@"D:\Programming\.NET\C#\NochesLanguage\Preprocessor\refined.txt");
foreach ((string name, string msg) in chatData)
{
    await file.WriteLineAsync($"{name}\t{msg}");
}