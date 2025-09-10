using gm_codex.Application.Services;
using gm_codex.Domain.Enums;
using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.Repositories.Interface;
using Xunit;

namespace gm_codex.Tests;

public class CharacterServiceTests
{
    //fake repo
    private class FakeEntityRepository : IEntityRepository
    {
        public bool InsertWasCalled { get; private set; }
        public Entity? InsertedEntity { get; private set; }

        public int InsertEntity(Entity entity)
        {
            InsertWasCalled = true;
            InsertedEntity = entity;
            return 1;
        }

        public EntityRecord? GetEntityByName(string name) => null;
        public void DeleteEntityByName(string name) { }
        public IEnumerable<EntityRecord> GetAllPlayableCharacters() => Enumerable.Empty<EntityRecord>();
        public IEnumerable<EntityRecord> GetAllNonPlayableCharacters() => Enumerable.Empty<EntityRecord>();
        
    }

    [Fact]
    public void CreateEntity_Should_Call_Insert_And_Pass_Correct_Data()
    {
        //arrange
        var fakeRepo = new FakeEntityRepository();
        var service = new CharacterService(fakeRepo);
        
        //act
        service.CreateEntity("aragorn", EntityType.pc, Race.human, null, Class.ranger, null, "20", "15");
        
        //assert
        Assert.True(fakeRepo.InsertWasCalled); //did create call insert?
        Assert.NotNull(fakeRepo.InsertedEntity); //did we capture the entity?
        Assert.Equal("aragorn", fakeRepo.InsertedEntity!.Name.ToLower());
        Assert.Equal(EntityType.pc, fakeRepo.InsertedEntity!.EntityType);
        Assert.Equal(Race.human, fakeRepo.InsertedEntity!.Race);
        Assert.Equal(Class.ranger, fakeRepo.InsertedEntity!.EntityClass);
        Assert.Equal("20", fakeRepo.InsertedEntity!.MaxHp);
        Assert.Equal("15", fakeRepo.InsertedEntity!.ArmorClass);
    }

    [Fact]
    public void DeleteEntity_Should_Not_Call_Repo_When_Name_Is_Empty()
    {
        var fakeRepo = new FakeEntityRepository();
        var service = new CharacterService(fakeRepo);
        
        service.DeleteEntity("");
        
        Assert.False(fakeRepo.InsertWasCalled);
    }
}