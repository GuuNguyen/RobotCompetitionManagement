using System;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using RobotCompetition.Repo;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

interface IRobotService
{
    void CreateTeam(Team team);
    List<TeamDTO> GetTeams();

    void DeleteTeam();

    void CreateCheckPoint(Checkpoint checkpoint);

    List<CheckpointDTO> GetCheckPoints();

    void CreateScore(Score score);

    List<ScoreDTO> GetScores();

    void UpdateCheckPoint(CheckpointDTO checkpoint);
    CheckpointDTO getCheckPointById(String id);
    TeamDTO getTeamById(String keyId);

    //new
    List<ScoreDTO> GetScoreByTeamCode(String TeamCode);
    //new
    void UpdateScores(List<Score> score, string teamId);

    List<ScoreDTO> GetScoreByCheckPointCode(String CheckpointCode);

    void UpdateScoreWhenCheckPointUpdate(CheckpointDTO checkpoint);
    void DeleteTeamById(string teamId);
    void DeleteCheckpointById(string checkpointId);
}
public class RobotService : IRobotService
{
    IFirebaseClient client;

    public RobotService()
    {
        IFirebaseConfig fcon = new FirebaseConfig()
        {
            AuthSecret = "FMbKegDkBxNy91bhrBdIjzDMuXuiBSWVWO4WfI2o",
            BasePath = "https://robotcompetition-be92d-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };
        client = new FireSharp.FirebaseClient(fcon);
    }

    public void CreateCheckPoint(Checkpoint checkpoint)
    {
        PushResponse response = client.Push("checkpoint", checkpoint);

    }

    public void CreateScore(Score score/*, string id*/)
    {
        PushResponse response = client.Push("score", score);
    }
    public List<TeamDTO> GetTeams()
    {
        FirebaseResponse response = client.Get("team");
        Dictionary<string, TeamDTO> data = response.ResultAs<Dictionary<string, TeamDTO>>();
        List<TeamDTO> teams = new List<TeamDTO>();
        foreach (var item in data)
        {
            teams.Add(new TeamDTO() { Name = item.Value.Name, Code = item.Value.Code, SchoolName = item.Value.SchoolName, Status = item.Value.Status ,keyId = item.Key });
        }
        return teams;

    }   
    public void CreateTeam(Team team)
    {
        PushResponse response = client.Push("team", team);
        Console.WriteLine(response.Result.Name);
        var check = response.Result.Name;
    }

    public void DeleteTeam()
    {
        FirebaseResponse response = client.Delete("team"); //Deletes todos collection
        Console.WriteLine(response);
    }

    public CheckpointDTO getCheckPointById(string id)
    {
        FirebaseResponse response = client.Get("checkpoint/" + id);
        CheckpointDTO checkpoint = response.ResultAs<CheckpointDTO>();
        checkpoint.Id = id;
        return checkpoint;
    }

    public List<CheckpointDTO> GetCheckPoints()
    {
        FirebaseResponse response = client.Get("checkpoint");
        Dictionary<string, CheckpointDTO> data = response.ResultAs<Dictionary<string, CheckpointDTO>>();
        List<CheckpointDTO> checkpoints = new List<CheckpointDTO>();
        foreach (var item in data)
        {
            checkpoints.Add(new CheckpointDTO() { Code = item.Value.Code, DefaultValue = item.Value.DefaultValue, Name = item.Value.Name, Id = item.Key });
        }
        return checkpoints;
    }

    public List<ScoreDTO> GetScoreByCheckPointCode(string CheckpointCode)
    {
        List<ScoreDTO> scores = GetScores();
        List<ScoreDTO> results = new List<ScoreDTO>();
        foreach (var item in scores)
        {
            if (item.CheckpointCode.Equals(CheckpointCode))
            {
                results.Add(item);
            }
        }
        return results;
    }

    public List<ScoreDTO> GetScoreByTeamCode(string TeamCode)
    {
        List<ScoreDTO> scores = GetScores();
        List<ScoreDTO> scoreByTeamId = new List<ScoreDTO>();
        if (scores == null) return null;
        foreach (ScoreDTO score in scores)
        {
            if (score.TeamCode == Int32.Parse(TeamCode))
            {
                scoreByTeamId.Add(score);
            }
        }
        return scoreByTeamId;
    }

    public List<ScoreDTO> GetScores()
    {
        FirebaseResponse response = client.Get("score");
        var check = response.Body;
        if (check != "null")
        {
            Dictionary<string, ScoreDTO> data = response.ResultAs<Dictionary<string, ScoreDTO>>();
            List<ScoreDTO> scores = new List<ScoreDTO>();
            foreach (var item in data)
            {
                scores.Add(new ScoreDTO() { CheckpointCode = item.Value.CheckpointCode, CheckTime = item.Value.CheckTime, ScoreValue = item.Value.ScoreValue, TeamCode = item.Value.TeamCode, KeyId = item.Key });
            }
            return scores;
        }
        return null;
    }


    public void UpdateCheckPoint(CheckpointDTO checkpoint)
    {
        var newCheckPoint = new Checkpoint() { Code = checkpoint.Code, DefaultValue = checkpoint.DefaultValue, Name = checkpoint.Name };
        client.Update("checkpoint/" + checkpoint.Id, newCheckPoint);
        UpdateScoreWhenCheckPointUpdate(checkpoint);
    }

    public void UpdateScores(List<Score> scores, string teamId)
    {
        List<ScoreDTO> oldScores = GetScoreByTeamCode(teamId);
        if (oldScores != null)
        {
            foreach (ScoreDTO oldScore in oldScores)
            {
                foreach (Score score in scores)
                {
                    if (score.CheckpointCode.Equals(oldScore.CheckpointCode))
                    {
                        score.CheckTime = oldScore.CheckTime;
                    }
                }
                client.Delete("score/" + oldScore.KeyId);
            }
        }
        foreach (Score newScore in scores)
        {
            CreateScore(newScore);
        }

    }

    public void UpdateScoreWhenCheckPointUpdate(CheckpointDTO checkpoint)
    {
        //get listscore -> get scores by check point code -> update all this checkpoint
        List<ScoreDTO> ScoreDTOs = GetScoreByCheckPointCode(checkpoint.Code);
        foreach (ScoreDTO ScoreDTO in ScoreDTOs)
        {
            client.Set("score/" + ScoreDTO.KeyId + "/ScoreValue", Int32.Parse(checkpoint.DefaultValue));
        }
    }


    public void DeleteTeamById(string teamId)
    {
        TeamDTO team = getTeamById(teamId);
        List<ScoreDTO> scores = GetScoreByTeamCode(team.Code.ToString());
        client.Delete("team/" + teamId);
        if (!(scores.Count() > 0))
        {
            return;
        }

        foreach (ScoreDTO scoreTeam in scores)
        {
            client.Delete("score/" + scoreTeam.KeyId);
        }

    }

    public TeamDTO getTeamById(string keyId)
    {
        FirebaseResponse response = client.Get("team/"+ keyId);
        TeamDTO team = response.ResultAs<TeamDTO>();
        team.keyId = keyId;
        return team;
    }

    public void DeleteCheckpointById(string checkpointId)
    {
        CheckpointDTO checkpoint = getCheckPointById(checkpointId);
        List<ScoreDTO> scores = GetScoreByCheckPointCode(checkpoint.Code);
        client.Delete("checkpoint/" + checkpointId);
        if (!(scores.Count() > 0))
        {
            return;
        }

        foreach (ScoreDTO scoreCheckpoint in scores)
        {
            client.Delete("score/" + scoreCheckpoint.KeyId);
        }
    }
}