using DeveloperEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DeveloperEvaluation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Compute(Compute model)
        {
            if (ModelState.IsValid)
            {
                if (model.NumbersWithComma != null)
                {
                    string NumbersWithComma = model.NumbersWithComma.TrimEnd(',');
                    var NumberStringArray = NumbersWithComma.Split(',');//Remove ',' from number entered by user
                 
                    List<int> NumberList = new List<int>();//Contains Numbers to be calculate
                    foreach (string number in NumberStringArray)
                    {
                        NumberList.Add(Convert.ToInt32(number));
                    }

                    //Compute and save result to ComputeModel
                    model.Average = GetAverage(NumberList);
                    model.Median = GetMedian(NumberList);
                    model.Mode = GetMode(NumberList);
                    return View("Index", model);
                }
                else
                    return View("Index");
            }
            else
                return View("Index");
        }
        public int? GetAverage(List<int> ListOfNumbers)
        {
            int? Average = null;
            if (ListOfNumbers != null)
            {
                int TotalOfNumbers = 0;
                foreach (int number in ListOfNumbers)
                    TotalOfNumbers += number;
                Average = TotalOfNumbers / ListOfNumbers?.Count();
            }
            return Average;
        }
        public int? GetMedian(List<int> ListOfNumbers)
        {
            //(1) Median is the middle number of a sorted List
            //(2) If more than one Median exists, the Median is the of the two numbers
            int? MedianValue = null;
            int MedianValueIndex;
            if (ListOfNumbers != null)
            {
                ListOfNumbers.Sort();
                MedianValueIndex = ListOfNumbers.Count() / 2;
                if (ListOfNumbers.Count() % 2 == 0) // Even Amount of Numbers - Two Medians Returned
                {
                    if (ListOfNumbers.Count > 1)
                        MedianValue = (ListOfNumbers[MedianValueIndex] + ListOfNumbers[MedianValueIndex - 1]) / 2;
                    else//If only 1 number then that number is the Median
                        MedianValue = ListOfNumbers[MedianValueIndex];
                }
                else // Odd Amount of Numbers - One Median Returned
                {
                    MedianValue = ListOfNumbers[MedianValueIndex];
                }
            }

            return MedianValue;
        }
        public List<int?> GetMode(List<int> ListOfNumbers)
        {
            //(1) Mode is the value that occurs most often
            //(2) There can be more than one Mode

            List<int?> Modes = new List<int?>();
            List<KeyValuePair<int?, int?>> NumberAndOccurrenceCounterList = new List<KeyValuePair<int?, int?>>();
            KeyValuePair<int?, int?> TempNumberKV = new KeyValuePair<int?, int?>();
            if (ListOfNumbers != null)
            {
                ListOfNumbers.Sort();
                int HighestNumberCounter = 0;
                foreach (int number in ListOfNumbers)
                {
                    //This store each number with its number of Occurrence
                    var TempNumberAndOccurrenceCounterList = ListOfNumbers.GroupBy(i => i)
                                                     .Where(x => x.Count() > 1)
                                                     .Select(y => new
                                                     {
                                                         key = y.Key,
                                                         Counter = y.Count()
                                                     }).ToList();
                    if (TempNumberAndOccurrenceCounterList != null)
                    {
                        int NumberCounter = TempNumberAndOccurrenceCounterList.Where(x => Convert.ToInt32(x.key.ToString()) == number).Select(y => y.Counter).FirstOrDefault();
                        TempNumberKV = new KeyValuePair<int?, int?>(number, Convert.ToInt32(NumberCounter));
                        NumberAndOccurrenceCounterList.Add(TempNumberKV);
                        if (HighestNumberCounter < Convert.ToInt32(NumberCounter))
                            HighestNumberCounter = Convert.ToInt32(NumberCounter);
                    }
                }

                //One or More Mode exists we display them all
                if (NumberAndOccurrenceCounterList?.Where(x => x.Value == HighestNumberCounter)?.Count() > 0
                    && NumberAndOccurrenceCounterList?.Where(x => x.Value == HighestNumberCounter)?.Count() != ListOfNumbers.Count())
                {
                    Modes = NumberAndOccurrenceCounterList?.Where(x => x.Value == HighestNumberCounter)?.Select(y => y.Key)?.Distinct().ToList();
                }
                else
                    Modes = null;
            }

            return Modes;
        }
    }
}
