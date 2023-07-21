using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Bet : MonoBehaviour
{
    int dogID;
    int amount;

    public int Amount { get { return amount; } private set { amount = value; } }

    public void SetBetting(int dogID, int amount)
    {
        this.dogID = dogID;
        this.amount = amount;
    }

    public string GetDescription()
    {
        // 누가 멍멍이 1번에게 100달러를 베팅했습니다.
        StringBuilder sb = new StringBuilder();
        sb.Append(transform.parent.name);
        sb.Append("가 멍멍이 ");
        sb.Append(dogID);
        sb.Append("번에게 ");
        sb.Append(amount);
        sb.Append("달러를 베팅했습니다.");
        return sb.ToString();
    }
}
