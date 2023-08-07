#include "Balance.h"

Balance::Balance(int _left, int _right)
{
    this->left =  _left ;
    this->right = _right ;
}

Balance::Balance()
{
    this->left = 0;
    this->right = 0;
}

Balance::Balance(const Balance& _other)
{
    this->left = _other.left;
    this->right = _other.right;
}

Balance* Balance::checkAmount(std::string amount)
{
    int checkRightLeft[3]{};

    size_t i = 0;
    for (; amount[i + 1] != '\0'; i++)
    {
        checkRightLeft[checkRightLeft[2]] += amount[i] - 48;
        if (amount[i + 1] == '.')
        {
            checkRightLeft[2]++;
            i++;
        }
        else
            checkRightLeft[checkRightLeft[2]] *= 10;
    }
    checkRightLeft[1] += amount[i] - 48;

    Balance* b = new Balance(checkRightLeft[0], checkRightLeft[1]);

    return b;
}
