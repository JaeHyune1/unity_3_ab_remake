using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExAccessControl : MonoBehaviour
{

    //public���� ���𤷵� ������ �ٸ� ��ũ��Ʈ���� ���� ���� ����
    public int publicVlaue;

    //private���� ����� ������ ���� Ŭ���� �������� ���� ����
    private int privateValue;

    //protected�� ����� ������ ���� Ŭ���� �� �Ļ� Ŭ�������� ���� ����
    protected int protectedValue;

    //internal�� ����� ������ ���� �����(������Ʈ �� �ٸ� ��ũ��Ʈ)������ ���� ����
    internal int internalValue;

}
