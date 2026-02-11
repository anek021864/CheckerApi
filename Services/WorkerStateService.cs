namespace JigNetApi;

public class WorkerStateService
{
    // เก็บเวลาล่าสุดที่ Worker ทำงานรอบล่าสุดเสร็จ
    public DateTime LastRunTime { get; set; } = DateTime.Now;
}
