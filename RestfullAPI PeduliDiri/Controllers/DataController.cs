﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfullAPI_PeduliDiri.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RestfullAPI_PeduliDiri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly string connectionString;
        SqlCommand cmd;
        SqlDataReader reader;

        public DataController(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionStrings:SqlDatabase"];
        }

        [HttpPost]
        public IActionResult createData(DataRequest dataRequest)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO tb_data (tanggal, jam, lokasi, suhu_tubuh, id_user) VALUES (@tanggal, @jam, @lokasi, @suhu_tubuh, @id_user)";
                    using (var command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@tanggal", dataRequest.tanggal);
                        command.Parameters.AddWithValue("@jam", dataRequest.jam);
                        command.Parameters.AddWithValue("@lokasi", dataRequest.lokasi);
                        command.Parameters.AddWithValue("@suhu_tubuh", dataRequest.suhu_tubuh);
                        command.Parameters.AddWithValue("@id_user", dataRequest.id_user);
                        command.ExecuteNonQuery();
                    }
                }
            } catch (Exception ex)
            {
                ModelState.AddModelError("Data", ex.Message);
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult GetData()
        {
            List<DataResponse> dataList = new List<DataResponse>();

            try
            {
                using(var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM tb_data";
                    using(reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataResponse data = new DataResponse();
                            data.id = reader.GetInt32("id");
                            data.tanggal = reader.GetDateTime("tanggal");
                            data.jam = reader.GetTimeSpan(reader.GetOrdinal("jam"));
                            data.lokasi = reader.GetString("lokasi");
                            data.suhu_tubuh = reader.GetDouble("suhu_tubuh");
                            data.id_user = reader.GetInt32("id_user");

                            dataList.Add(data);
                        }
                    }
                }
            } catch (Exception ex)
            {
                ModelState.AddModelError("Data", ex.Message);
                return BadRequest(ModelState);
            }

            return Ok(dataList);
        }

        [HttpPost("{id}")]
        public IActionResult GetDataByID(int id)
        {
            DataResponse response = new DataResponse();

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM tb_data WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            response.id = reader.GetInt32("id");
                            response.tanggal = reader.GetDateTime("tanggal");
                            response.jam = reader.GetTimeSpan(reader.GetOrdinal("jam"));
                            response.lokasi = reader.GetString("lokasi");
                            response.suhu_tubuh = reader.GetDouble("suhu_tubuh");
                            response.id_user = reader.GetInt32("id_user");
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Data", ex.Message);
                return BadRequest(ModelState);
            }

            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult updateDataByID(DataRequest dataRequest, int id)
        {
            try
            {
                using(var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "UPDATE tb_data SET tanggal = @tanggal, jam = @jam, lokasi = @lokasi, suhu_tubuh = @suhu_tubuh WHERE id = @id";
                    cmd.Parameters.AddWithValue("@tanggal", dataRequest.tanggal);
                    cmd.Parameters.AddWithValue("@jam", dataRequest.jam);
                    cmd.Parameters.AddWithValue("@lokasi", dataRequest.lokasi);
                    cmd.Parameters.AddWithValue("@suhu_tubuh", dataRequest.suhu_tubuh);
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }
            } catch (Exception ex)
            {
                ModelState.AddModelError("Data", ex.Message);
                return BadRequest(ModelState);
            }

            return Ok();
        } 
    }
}
