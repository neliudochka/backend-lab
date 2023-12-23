using backend_lab.Models;

namespace backend_lab.Services;

public class RecordService {
    private readonly List<Record> _records = new()
    {};
    
    public Record GetRecordById(Guid id)
    {
        return _records.FirstOrDefault(c => c.Id == id);
    }
    
    public bool DeleteRecordById(Guid id)
    {
        var record = GetRecordById(id);
        if (record != null)
        {
            _records.Remove(record);
            return true;
        }
        return false;
    }
    
    //POST /record
    public void AddRecord(Record record)
    {
        if (GetRecordById(record.Id) is not null)
            return;
        _records.Add(record);
    }
    
    //GET /record 
    public IEnumerable<Record> GetRecords(RecordRequest request)
    {
        if (request.userId is null)
            return _records.Where(i => i.CategoryId == request.categoryId);
        if (request.categoryId is null)
            return _records.Where(i => i.UserId == request.userId);
        return _records.Where(i => i.CategoryId == request.categoryId)
            .Where(i => i.UserId == request.userId);
    }

    
}