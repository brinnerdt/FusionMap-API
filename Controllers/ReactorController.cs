using Dapper;
using FusionMapAPI.Data;
using FusionMapAPI.Dtos;
using FusionMapAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FusionMapAPI.Controllers
{
    public class ReactorController(IConfiguration config) : ControllerBase
    {
        DataContextDapper _dapper = new DataContextDapper(config);
        [HttpGet("GetReactors")]
        public IEnumerable<Reactor> GetReactors()
        {
            string sql = @"SELECT * FROM reactor";
            IEnumerable<Reactor> reactors = _dapper.LoadData<Reactor>(sql);
            return reactors;
        }

        [HttpGet("GetReactors/{reactorId}")]
        public Reactor GetSingleReactor(int reactorId)
        {
            string sql = @"SELECT * FROM reactor WHERE ReactorId = @reactorid";
            var parameters = new DynamicParameters();
            parameters.Add("reactorid", reactorId);
            Reactor reactor = _dapper.LoadDataSingle<Reactor>(sql, parameters);
            return reactor;
        }

        [HttpPut("UpdateReactor")]
        public IActionResult UpdateReactor(Reactor reactor)
        {
            string sql = @"
                UPDATE reactor
                    SET plantid = @PlantId,
                    InspectionId = @InspectionId,
                    MaxPowerOutput = @MaxPowerOutput,
                    LeadEngineerId = @LeadEngineerId,
                    RadiationLevel = @RadiationLevel
                WHERE ReactorId = @ReactorId";

            var parameters = new DynamicParameters();
            parameters.Add("ReactorId", reactor.ReactorId);
            parameters.Add("PlantId", reactor.PlantId);
            parameters.Add("InspectionId", reactor.InspectionId);
            parameters.Add("MaxPowerOutput", reactor.MaxPowerOutput);
            parameters.Add("LeadEngineerId", reactor.LeadEngineerId);
            parameters.Add("RadiationLeve", reactor.RadiationLevel);

            if (_dapper.ExecuteSql(sql, parameters))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("AddReactor")]
        public IActionResult AddReactor(ReactorDto reactor)
        {
            string sql = @"
                INSERT INTO reactor (
                    PlantId, 
                    InspectionId, 
                    MaxPowerOutput, 
                    LeadEngineerId, 
                    RadiationLevel)
                VALUES (@PlantId, 
                @InspectionId, 
                @MaxPowerOutput, 
                @LeadEngineerId, 
                @RadiationLevel
            )";

            var parameters = new DynamicParameters();
            parameters.Add("PlantId", reactor.PlantId);
            parameters.Add("InspectionId", reactor.InspectionId);
            parameters.Add("MaxPowerOutput", reactor.MaxPowerOutput);
            parameters.Add("LeadEngineerId", reactor.LeadEngineerId);
            parameters.Add("RadiationLevel", reactor.RadiationLevel);

            if (_dapper.ExecuteSql(sql, parameters))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteReactor/{reactorId}")]
        public IActionResult DeleteReactor(int reactorId)
        {
            string sql = @"DELETE FROM reactor WHERE ReactorId = @reactorid";
            var parameters = new DynamicParameters();
            parameters.Add("reactorid", reactorId);
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