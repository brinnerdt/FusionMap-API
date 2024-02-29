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
                    Status = @Status,
                    ReferenceUnitPower = @ReferenceUnitPower,
                    GrossElectricalCapacity = @GrossElectricalCapacity,
                    FirstGridConnection = @FirstGridConnection
                    Latitude = @Latitude,
                    Longitude = @Longitude
                WHERE PlantId = @PlantId";

            var parameters = new DynamicParameters();
            parameters.Add("PlantId", plant.PlantId);
            parameters.Add("Name", plant.Name);
            parameters.Add("Status", plant.Status);
            parameters.Add("ReferenceUnitPower", plant.ReferenceUnitPower);
            parameters.Add("GrossElectricalCapacity", plant.GrossElectricalCapacity);
            parameters.Add("FirstGridConnection", plant.FirstGridConnection);
            parameters.Add("Latitude", plant.Latitude);
            parameters.Add("Longitude", plant.Longitude);

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
                    Status, 
                    ReferenceUnitPower, 
                    GrossElectricalCapacity, 
                    FirstGridConnection,
                    Latitude,
                    Longitude
                ) VALUES (
                    @Name, 
                    @Status, 
                    @ReferenceUnitPower, 
                    @GrossElectricalCapacity, 
                    @FirstGridConnection,
                    @Latitude,
                    @Longitude
                )";

            var parameters = new DynamicParameters();
            parameters.Add("Name", plant.Name);
            parameters.Add("Status", plant.Status);
            parameters.Add("ReferenceUnitPower", plant.ReferenceUnitPower);
            parameters.Add("GrossElectricalCapacity", plant.GrossElectricalCapacity);
            parameters.Add("FirstGridConnection", plant.FirstGridConnection);
            parameters.Add("Latitude", plant.Latitude);
            parameters.Add("Longitude", plant.Longitude);

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

        [HttpGet("PlantRadiations")]
        public IEnumerable<PlantReactorDto> GetPlantRadiations()
        {
            string sql = @"
                SELECT 
                    p.Name,
                    AVG(r.RadiationLevel) AS AvgReactorRadiation
                FROM plant p
                JOIN reactor r ON p.PlantId = r.PlantId
                GROUP BY p.Name
                ORDER BY AvgReactorRadiation DESC
                LIMIT 10;";

            IEnumerable<PlantReactorDto> plantRadiations = _dapper.LoadData<PlantReactorDto>(sql);
            return plantRadiations;
        }

        [HttpGet("PlantCountByYear")]
        public IEnumerable<PlantCountByYearDto> GetPlantCountByYear()
        {
            string sql = @"
                SELECT EXTRACT(YEAR FROM firstgridconnection) AS year, COUNT(*)
                FROM plant
                GROUP BY year
                ORDER BY year;";

            IEnumerable<PlantCountByYearDto> plantCountByYear = _dapper.LoadData<PlantCountByYearDto>(sql);
            return plantCountByYear;
        }
    }
}