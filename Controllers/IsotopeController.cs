using Dapper;
using FusionMapAPI.Data;
using FusionMapAPI.Dtos;
using FusionMapAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FusionMapAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IsotopeController(IConfiguration config) : ControllerBase
    {
        DataContextDapper _dapper = new DataContextDapper(config);

        [HttpGet("GetIsotopes")]
        public IEnumerable<Isotope> GetIsotopes()
        {
            string sql = @"SELECT * FROM isotope";
            IEnumerable<Isotope> isotopes = _dapper.LoadData<Isotope>(sql);
            return isotopes;
        }

        [HttpGet("GetIsotopes/{isotopeId}")]
        public Isotope GetSingleIsotope(int isotopeId)
        {
            string sql = @"SELECT * FROM isotope WHERE IsotopeId = @isotopeid";
            var parameters = new DynamicParameters();
            parameters.Add("isotopeid", isotopeId);
            Isotope isotope = _dapper.LoadDataSingle<Isotope>(sql, parameters);
            return isotope;
        }

        [HttpPut("UpdateIsotope")]
        public IActionResult UpdateIsotope(Isotope isotope)
        {
            string sql = @"
                UPDATE isotope
                    SET Name = @Name,
                    Symbol = @Symbol
                WHERE IsotopeId = @IsotopeId";

            var parameters = new DynamicParameters();
            parameters.Add("IsotopeId", isotope.IsotopeId);
            parameters.Add("Name", isotope.Name);
            parameters.Add("Symbol", isotope.Symbol);

            if (_dapper.ExecuteSql(sql, parameters))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("AddIsotope")]
        public IActionResult AddIsotope(IsotopeDto isotope)
        {
            string sql = @"
                INSERT INTO isotope (Name, Symbol)
                VALUES (@Name, @Symbol)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", isotope.Name);
            parameters.Add("Symbol", isotope.Symbol);

            if (_dapper.ExecuteSql(sql, parameters))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("DeleteIsotope/{isotopeId}")]
        public IActionResult DeleteIsotope(int isotopeId)
        {
            string sql = @"DELETE FROM isotope WHERE IsotopeId = @isotopeid";
            var parameters = new DynamicParameters();
            parameters.Add("isotopeid", isotopeId);
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