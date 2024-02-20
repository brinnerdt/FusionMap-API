using Dapper;
using FusionMapAPI.Data;
using FusionMapAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FusionMapAPI.Dtos
{
    [ApiController]
    [Route("[controller]")]
    public class InspectionTypeController(IConfiguration config) : ControllerBase
    {
        DataContextDapper _dapper = new DataContextDapper(config);

        [HttpGet("GetInspectionTypes")]
        public IEnumerable<InspectionType> GetInspectionTypes()
        {
            string sql = @"SELECT * FROM inspection_type";
            IEnumerable<InspectionType> inspectionTypes = _dapper.LoadData<InspectionType>(sql);
            return inspectionTypes;
        }

        [HttpGet("GetInspectionTypes/{inspectionTypeId}")]
        public InspectionType GetSingleInspectionType(int inspectionTypeId)
        {
            string sql = @"SELECT * FROM inspection_type WHERE InspectionTypeId = @inspectiontypeid";
            var parameters = new DynamicParameters();
            parameters.Add("inspectiontypeid", inspectionTypeId);
            InspectionType inspectionType = _dapper.LoadDataSingle<InspectionType>(sql, parameters);
            return inspectionType;
        }

        [HttpPut("UpdateInspectionType")]
        public IActionResult UpdateInspectionType(InspectionType inspectionType)
        {
            string sql = @"
                UPDATE inspection_type
                    SET Name = @Name,
                    Description = @Description,
                    Cadence = @Cadence
                WHERE InspectionTypeId = @InspectionTypeId";

            var parameters = new DynamicParameters();
            parameters.Add("InspectionTypeId", inspectionType.InspectionId);
            parameters.Add("Name", inspectionType.Name);
            parameters.Add("Description", inspectionType.Description);
            parameters.Add("Cadence", inspectionType.Cadence);

            if (_dapper.ExecuteSql(sql, parameters))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("AddInspectionType")]
        public IActionResult AddInspectionType(InspectionType inspectionType)
        {
            string sql = @"
                INSERT INTO inspection_type (Name, Description, Cadence)
                VALUES (@Name, @Description, @Cadence)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", inspectionType.Name);
            parameters.Add("Description", inspectionType.Description);
            parameters.Add("Cadence", inspectionType.Cadence);

            if (_dapper.ExecuteSql(sql, parameters))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteInspectionType/{inspectionTypeId}")]
        public IActionResult DeleteInspectionType(int inspectionTypeId)
        {
            string sql = @"DELETE FROM inspection_type WHERE InspectionTypeId = @inspectiontypeid";
            var parameters = new DynamicParameters();
            parameters.Add("inspectiontypeid", inspectionTypeId);
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