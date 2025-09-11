using gm_codex.Application.Services;
using gm_codex.Domain.Enums;
using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.Repositories.Interface;

using Moq;
using Xunit;

namespace gm_codex.Tests.Application.Services;


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

        public int UpdateEntity(Entity entity)
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
    public void CreateEntity_Should_Throw_When_Name_Is_Empty()
    {
        var fakeRepo = new FakeEntityRepository();
        var service = new CharacterService(fakeRepo);

        Assert.Throws<ArgumentException>(() =>
            service.CreateEntity("", EntityType.pc, Race.human, null, Class.ranger, null, "20", "15"));
    }
    
    [Fact]
    public void CreateEntity_Should_Call_Insert_And_Pass_Correct_Data()
    {
        //arrange

        var mockRepo = new Mock<IEntityRepository>();
        
        //Setup default behavior: InsertEntity always "succeeds"
        mockRepo.Setup(r => r.InsertEntity(It.IsAny<Entity>()))
            .Returns(1);
        
        var service = new CharacterService(mockRepo.Object);

        
        //act
        service.CreateEntity("aragorn", EntityType.pc, Race.human, null, Class.ranger, null, "20", "15");
        
        //assert
        //verify InsertEntity was called exactly once with an Entity matching these expectations
        mockRepo.Verify(r =>
                r.InsertEntity(It.Is<Entity>(e =>
                    e.Name == "aragorn" &&
                    e.EntityType == EntityType.pc &&
                    e.Race == Race.human &&
                    e.EntityClass == Class.ranger &&
                    e.MaxHp == "20" &&
                    e.ArmorClass == "15"
                )),
            Times.Once);

    }

    [Fact]
    public void UpdateEntity_Should_Throw_When_Name_Is_Empty()
    {
        var fakeRepo = new FakeEntityRepository();
        var service = new CharacterService(fakeRepo);
        
        Assert.Throws<ArgumentException>(() =>
            service.UpdateEntity("", EntityType.pc, Race.human, null, Class.ranger, null, "20", "15"));
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