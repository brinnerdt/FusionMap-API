using Dapper;
using FusionMapAPI.Data;
using FusionMapAPI.Dtos;
using FusionMapAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FusionMapAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlantController(IConfiguration config) : ControllerBase
    {
        DataContextDapper _dapper = new DataContextDapper(config);

        [HttpGet("GetPlants")]
        public IEnumerable<Plant> GetPlants()
        {
            string sql = @"SELECT * FROM plant";
            IEnumerable<Plant> plants = _dapper.LoadData<Plant>(sql);
            return plants;
        }

        [HttpGet("GetPlants/{plantId}")]
        public Plant GetSinglePlant(int plantId)
        {
            string sql = @"SELECT * FROM plant WHERE PlantId = @plantid";
            var parameters = new DynamicParameters();
            parameters.Add("plantid", plantId);
            Plant plant = _dapper.LoadDataSingle<Plant>(sql, parameters);
            return plant;
        }

        [HttpPut("UpdatePlant")]
        public IActionResult UpdatePlant(Plant plant)
        {
            string sql = @"
                UPDATE plant
                    SET Name = @Name,
                    StateId = @StateId,
                    Status = @Status,
                    ReferenceUnitPower = @ReferenceUnitPower,
                    GrossElectricalCapacity = @GrossElectricalCapacity,
                    FirstGridConnection = @FirstGridConnection
                WHERE PlantId = @PlantId";

            var parameters = new DynamicParameters();
            parameters.Add("PlantId", plant.PlantId);
            parameters.Add("Name", plant.Name);
            parameters.Add("StateId", plant.StateId);
            parameters.Add("Status", plant.Status);
            parameters.Add("ReferenceUnitPower", plant.ReferenceUnitPower);
            parameters.Add("GrossElectricalCapacity", plant.GrossElectricalCapacity);
            parameters.Add("FirstGridConnection", plant.FirstGridConnection);

            if (_dapper.ExecuteSql(sql, parameters))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("AddPlant")]
        public IActionResult AddPlant(PlantDto plant)
        {
            string sql = @"
                INSERT INTO plant (
                    Name, 
                    StateId, 
                    Status, 
                    ReferenceUnitPower, 
                    GrossElectricalCapacity, 
                    FirstGridConnection
                ) VALUES (
                    @Name, 
                    @StateId, 
                    @Status, 
                    @ReferenceUnitPower, 
                    @GrossElectricalCapacity, 
                    @FirstGridConnection
                )";

            var parameters = new DynamicParameters();
            parameters.Add("Name", plant.Name);
            parameters.Add("StateId", plant.StateId);
            parameters.Add("Status", plant.Status);
            parameters.Add("ReferenceUnitPower", plant.ReferenceUnitPower);
            parameters.Add("GrossElectricalCapacity", plant.GrossElectricalCapacity);
            parameters.Add("FirstGridConnection", plant.FirstGridConnection);

            if (_dapper.ExecuteSql(sql, parameters))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeletePlant/{plantId}")]
        public IActionResult DeletePlant(int plantId)
        {
            string sql = @"DELETE FROM plant WHERE PlantId = @plantid";
            var parameters = new DynamicParameters();
            parameters.Add("plantid", plantId);
            if (_dapper.ExecuteSql(sql, parameters))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}