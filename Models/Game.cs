using System;
using System.Collections.Generic;
using System.Linq;
using LB_151.Data;

namespace LB_151.Models
{
    public class Game
    {
        const int NumberOfQuestions = 2;

        private const int TimeToAnswer = 23;

        private IList<Question> _questions;

        private int _questionIndex = 0;
        private int _points = 0;
        private bool _jokerUsed = false;
        private DateTime _timestamp;
        private DateTime? _questionTimestamp;
        private bool _gameEnded = false;
        private bool _submitted = false;
        private Result _currentResult;
        private Category _category;
        private readonly CategoryDatabaseService _categoryDatabaseService;
        private double _duration;

        public Game(Category category, CategoryDatabaseService categoryDatabaseService)
        {
            _category = category;
            _categoryDatabaseService = categoryDatabaseService;
            _questions = category.Questions.Shuffle();
        }

        public Game StartGame()
        {
            _timestamp = DateTime.Now;
            return this;
        }

        public string[] UseJoker()
        {
            var toReturn = new string[2].ToList();

            if (!_jokerUsed)
            {
                int i = 0;
                foreach (var s in _questions[_questionIndex].Answers.ToList().Shuffle())
                {
                    if (!s.Equals(_questions[_questionIndex].Correct))
                    {
                        toReturn.Add(s);
                        i++;
                        if (i >= 2) break;
                    }
                }

                _jokerUsed = true;
            }

            return toReturn.ToArray();
        }

        public Question GetCurrentQuestion()
        {
            var toReturn = _questions[_questionIndex].Clone();

            if (_questionTimestamp == null)
            {
                _questionTimestamp = DateTime.Now;
            }

            toReturn.Asked = _questionTimestamp.Value;

            return toReturn;
        }

        public void SubmitAnswer(string answer)
        {
            Result result = new Result() {Type = ResultType.WRONG};

            result.Correct = _questions[_questionIndex].Correct;

            if (!_submitted && !_gameEnded)
            {
                if ((DateTime.Now - _questionTimestamp.Value).TotalSeconds <= TimeToAnswer)
                {
                    // Todo: save to database whether answered correct or incorrect
                    if (_questions[_questionIndex].Correct.Equals(answer))
                    {
                        _points += 30;
                        if (_questionIndex < NumberOfQuestions - 1)
                        {
                            result.Type = ResultType.CORRECT;
                        }
                        else
                        {
                            result.Type = ResultType.WON;
                            _duration = (DateTime.Now - _timestamp).TotalSeconds;
                        }

                        _questions[_questionIndex].Right++;
                    }
                    else
                    {
                        result.Type = ResultType.WRONG;
                        _gameEnded = true;
                        _duration = (DateTime.Now - _timestamp).TotalSeconds;

                        _questions[_questionIndex].Wrong++;
                    }
                }
                else
                {
                    result.Type = ResultType.SLOW;
                    _gameEnded = true;
                    _duration = (DateTime.Now - _timestamp).TotalSeconds;
                    _questions[_questionIndex].Wrong++;
                }

                _questionIndex++;
                _currentResult = result;
            }

            //In case you are wondering, yes, this does indeed shuffle the whole database every time,
            //I could do this properly but it's 12 am and I have already spent way too much time on this.
            //And after all this is way funnier anyways.
            //Todo implement proper list comprehension to update values performance friendly
            _category.Questions = _questions.ToArray();
            _categoryDatabaseService.Update(_category.Id, _category);

            _questionTimestamp = null;
        }

        public Result GetCurrentResult()
        {
            return _currentResult;
        }

        public int GetPoints()
        {
            return _points;
        }

        public Category GetCategory()
        {
            return _category;
        }

        public double GetDuration()
        {
            return _duration;
        }

        public DateTime GetTimestamp()
        {
            return _timestamp;
        }
    }

    public class Result
    {
        public ResultType Type { get; set; }
        public string Correct { get; set; }
    }

    public enum ResultType
    {
        CORRECT,
        WRONG,
        SLOW,
        WON
    }
}