//This is a script used to get the leaderboard from an external source, store it in a list, and then sort the list from highest to lowest score. 

//Using a range of libraries to make the code work
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Linq;

//Creating two serializable classes for the user and object properties
[Serializable]
public class UserPropertyLeader
{
    //Declaring the user property fields
    public string name;
    public string score;
}
[Serializable]
public class ObjectProperty
{
    //Declaring the object property fields
    public UserPropertyLeader[] myUsers;
}

public class GetLeaderBoard : MonoBehaviour
{
    //Declaring the text arrays, lists, and strings to be used
    public Text[] LeaderBoardText;
    public List<int> UnsortedList = new List<int>();
    public  int[] sortedList;

    public int position1score;
    public int position2score;
    public int position3score;
    public int position4score;
    public int position5score;

    public string position1name;
    public string position2name;
    public string position3name;
    public string position4name;
    public string position5name;

    public string position1;
    public string position2;
    public string position3;
    public string position4;
    public string position5;

    public Text[] orderedTable;
    public string Url;

    public int forLoop_index = 0;

    //Creating a list to store the leaderboard
    List<UserPropertyLeader> ListBoard = new List<UserPropertyLeader>();
    ObjectProperty s = new ObjectProperty();   
    // Start is called before the first frame update
    void Start()
    {
        //Calling the GetLeader function
        StartCoroutine(GetLeader());
    }
    IEnumerator GetLeader()
    {
        //Creating a new web request
        UnityWebRequest www = UnityWebRequest.Get(Url);
        yield return www.SendWebRequest();
        if(www.isDone)
        {
            //Storing the web request result
            var result = www.downloadHandler.text;
            //Debug.Log(result);
            if(result != "0")
            {
                //Creating a json array and converting it to a string
                var JsonArrayString = "{\"myUsers\":" + result + "}";
                //Converting the json array to an object
                s = JsonUtility.FromJson<ObjectProperty>(JsonArrayString);
                //Debug.Log("Length");
                //Debug.Log(s.myUsers.Length);
                //Looping through the object and storing it in the list
                for (int i = 0; i < s.myUsers.Length; i++)
                {
                    ListBoard.Add(new UserPropertyLeader { name = s.myUsers[i].name, score = s.myUsers[i].score});    
                }
                //Calling the ShowLeaderboard function
                ShowLeaderboard();
            }
            else
            {
                Debug.Log("Error");
            }
        }
    }
    public void ShowLeaderboard()
    {   
        //Displaying the leaderboard on the Text objects
        LeaderBoardText[0].text = ListBoard.ElementAt(0).name + "score : " + ListBoard.ElementAt(0).score;
        LeaderBoardText[1].text = ListBoard.ElementAt(1).name + "score : " + ListBoard.ElementAt(1).score;
        LeaderBoardText[2].text = ListBoard.ElementAt(2).name + "score : " + ListBoard.ElementAt(2).score;
        LeaderBoardText[3].text = ListBoard.ElementAt(3).name + "score : " + ListBoard.ElementAt(3).score;
        LeaderBoardText[4].text = ListBoard.ElementAt(4).name + "score : " + ListBoard.ElementAt(4).score;
        Debug.Log("Position: " + LeaderBoardText[0].text);
        Debug.Log("Position: " + LeaderBoardText[1].text);
        Debug.Log("Position: " + LeaderBoardText[2].text);
        Debug.Log("Position: " + LeaderBoardText[3].text);
        Debug.Log("Position: " + LeaderBoardText[4].text);

        //Calling the unsortedArrayCreator function
        unsortedArrayCreator(); 
    }
    public int[] SortArray(int[] array, int left, int right)
    {
        if (left < right)
        {
            //Calculating the middle of the array
            int middle = left + (right - left) / 2;
            //Recursively calling the SortArray function
            SortArray(array, left, middle);
            SortArray(array, middle + 1, right);
            //Calling the MergeArray function
            MergeArray(array, left, middle, right);
        }
        //USED FOR TESTING - Debug.Log("Array line : "+ string.Join(" ", array));
        return array;
    }
    public void MergeArray(int[] array, int left, int middle, int right)
    {
        //Calculating the length of the left and right arrays
        var leftArrayLength = middle - left + 1;
        var rightArrayLength = right - middle;
        //Creating the left and right temp arrays
        var leftTempArray = new int[leftArrayLength];
        var rightTempArray = new int[rightArrayLength];
        int i, j;
        //Adding the elements of the left array to the left temp array
        for (i = 0; i < leftArrayLength; ++i)
            leftTempArray[i] = array[left + i];
        //Adding the elements of the right array to the right temp array
        for (j = 0; j < rightArrayLength; ++j)
            rightTempArray[j] = array[middle + 1 + j];
        i = 0;
        j = 0;
        int k = left;
        //Looping through the left and right temp arrays and comparing the elements and adding them to the array
        while (i < leftArrayLength && j < rightArrayLength)
        {
            if (leftTempArray[i] <= rightTempArray[j])
            {
                array[k++] = leftTempArray[i++];
            }
            else
            {
                array[k++] = rightTempArray[j++];
            }
        }
        //Looping through the left temp array and adding the elements to the array
        while (i < leftArrayLength)
        {
            array[k++] = leftTempArray[i++];
        }
        //Looping through the right temp array and adding the elements to the array
        while (j < rightArrayLength)
        {
            array[k++] = rightTempArray[j++];
        }
    }
    public void unsortedArrayCreator()
    {
        //This method creates an unsorted array from a list of objects.
        UnsortedList[0] = int.Parse(ListBoard.ElementAt(0).score); 
        //This line parses the score of the first element in the ListBoard and assigns it to the first element in the UnsortedList array.
        UnsortedList[1] = int.Parse(ListBoard.ElementAt(1).score); 
        //This line parses the score of the second element in the ListBoard and assigns it to the second element in the UnsortedList array.
        UnsortedList[2] = int.Parse(ListBoard.ElementAt(2).score); 
        //This line parses the score of the third element in the ListBoard and assigns it to the third element in the UnsortedList array.
        UnsortedList[3] = int.Parse(ListBoard.ElementAt(3).score); 
        //This line parses the score of the fourth element in the ListBoard and assigns it to the fourth element in the UnsortedList array.
        UnsortedList[4] = int.Parse(ListBoard.ElementAt(4).score); 
        //This line parses the score of the fifth element in the ListBoard and assigns it to the fifth element in the UnsortedList array.

        GetLeaderBoard sortFunction = new GetLeaderBoard(); 
        //This line creates a new instance of GetLeaderboard class called sortFunction 
        sortedList = sortFunction.SortArray(UnsortedList.ToArray(), 0,  UnsortedList.Count() -1); 
        //This line calls SortArray method from GetLeaderboard class and passes Unsortedlist array as a parameter, 
        //then assigns sorted list to sortedlist variable 

        LeaderBoardText[0].text = ListBoard.ElementAt(0).name + " " + ListBoard.ElementAt(0).score; 
        //This line sets text for LeaderboardText at index 0 with name and score from first element of Listboard 
        LeaderBoardText[1].text = ListBoard.ElementAt(1).name + " " + ListBoard.ElementAt(1).score; 
        //This line sets text for LeaderboardText at index 1 with name and score from second element of Listboard 
        LeaderBoardText[2].text = ListBoard.ElementAt(2).name + " " + ListBoard.ElementAt(2).score; 
        //This line sets text for LeaderboardText at index 2 with name and score from third element of Listboard 
        LeaderBoardText[3].text = ListBoard.ElementAt(3).name + " " + ListBoard.ElementAt(3).score; 
        //This line sets text for LeaderboardText at index 3 with name and score from fourth element of Listboard 
        LeaderBoardText[4].text = ListBoard.ElementAt(4).name + " " + ListBoard.ElementAt(4).score; 
        //This line sets text for LeaderboardText at index 4 with name and score from fourth element of Listboard 

        // This code assigns the values of the first five elements in the sortedList array to variables. 
        // position1score is assigned the value of the first element in the sortedList array.
        position1score = sortedList[0];

        // position2score is assigned the value of the second element in the sortedList array.
        position2score = sortedList[1];

        // position3score is assigned the value of the third element in the sortedList array.
        position3score = sortedList[2];

        // position4score is assigned the value of the fourth element in the sortedList array.
        position4score = sortedList[3];

        // position5score is assigned the value of the fifth element in the sortedList array. 
        position5score = sortedList[4];

        //This loop iterates through the unsorted list of integers. 
        foreach (int l in UnsortedList)
        {   
            //If the current integer in the loop is equal to the first element in the sorted list, then set position1name to the name of the element at the current index of ListBoard. 
            if( l == sortedList[0])
            {
                position1name = ListBoard.ElementAt(forLoop_index).name;
                //Increment forLoop_index by 1. 
                forLoop_index = forLoop_index + 1;
                        
            }
            //If the current integer in the loop is equal to the second element in the sorted list, then set position2name to the name of the element at the current index of ListBoard. 
            if( l == sortedList[1])
            {
                position2name = ListBoard.ElementAt(forLoop_index).name;
                //Increment forLoop_index by 1. 
                forLoop_index = forLoop_index + 1;
            }

            //If the current integer in the loop is equal to the third element in the sorted list, then set position3name to the name of the element at the current index of ListBoard. 
            if( l == sortedList[2])
            {
                position3name = ListBoard.ElementAt(forLoop_index).name;
                //Increment forLoop_index by 1. 
                forLoop_index = forLoop_index + 1;

            }

            //If the current integer in the loop is equal to the fourth element in the sorted list, then set position4name to the name of the element at the current index of ListBoard. 
            if( l == sortedList[3])
            {
                position4name = ListBoard.ElementAt(forLoop_index).name;

                //Increment forLoop_index by 1. 
                forLoop_index = forLoop_index + 1;

            }
            //If the current integer in the loop is equal to the fifth element in the sorted list, then set position5name to the name of the element at the current index of ListBoard. 

            if( l == sortedList[4])
            {
                position5name = ListBoard.ElementAt(forLoop_index).name;
                forLoop_index = forLoop_index + 1;
            }
        }
        //formatting of the scores
        position1 = position1name + " : " + position1score;
        position2 = position2name + " : " + position2score;
        position3 = position3name + " : " + position3score;
        position4 = position4name + " : " + position4score;
        position5 = position5name + " : " + position5score;

        orderedTable[4].text = position1name + " : " + position1score;
        orderedTable[3].text = position2name + " : " + position2score;
        orderedTable[2].text = position3name + " : " + position3score;
        orderedTable[1].text = position4name + " : " + position4score;
        orderedTable[0].text = position5name + " : " + position5score;
    }
}