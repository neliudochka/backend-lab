using Models;

namespace backend_lab.Services;

public class RecordService {
    private readonly RepoPull _repoPull;
    public RecordService(RepoPull repoPull)
    {
        _repoPull = repoPull;
    }

    public Task<Record> GetRecordById(Guid id)
    {
        return _repoPull.RecordRepo.GetByIdAsync(id);
    }
    
    public Task DeleteRecordById(Guid id)
    {
        return _repoPull.RecordRepo.DeleteByIdAsync(id);
    }
    
    //POST /record
    public async Task AddRecord(Record record)
    {
        await _repoPull.RecordRepo.AddAsync(record);
    }
    
    //GET /record 
    public async Task<IEnumerable<Record>> GetRecords(RecordRequest request)
    {
        var records = await _repoPull.RecordRepo.GetAllAsync();
        
        if (request.userId is null)
            return records.Where(i => i.CategoryId == request.categoryId);
        if (request.categoryId is null)
            return records.Where(i => i.UserId == request.userId);
        return records.Where(i => i.CategoryId == request.categoryId)
            .Where(i => i.UserId == request.userId);
    }

    
}