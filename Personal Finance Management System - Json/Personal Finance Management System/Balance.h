#pragma once
#include <iostream>
#include "nlohmann/json.hpp"

using json = nlohmann::json;
class Balance
{
private:
	int right{};
	int left{};
public:
	Balance(int _left, int _right);

	Balance();

	Balance(const Balance& _other);


	void operator+=(const Balance _Balance)
	{
		this->left += _Balance.left;
		this->right += _Balance.right;
		if (this->right >= 100)
		{
			this->left += 1;
			this->right -= 100;
		}
	}

	void operator -=(const Balance _Balance)
	{
		this->left -= _Balance.left;
		if (this->right <= _Balance.right)
		{
			this->left -= 1;
			this->right += 100;
			this->right = this->right % 100;
		}
	}

	bool operator <(const Balance _Balance)
	{
		return this->left < _Balance.left || this->right < _Balance.right;
	}

	bool operator >(const Balance _Balance)
	{
		return this->left > _Balance.left || this->right > _Balance.right;
	}

	friend std::ostream& operator<<(std::ostream& os, const Balance _Balance)
	{
		os << _Balance.left << '.' << _Balance.right;
		return os;
	}

	friend json& operator<<(json& jsonData, const Balance _Balance)
	{

		jsonData["left"] = _Balance.left;
		jsonData["right"] = _Balance.right;
		return jsonData;
	}

	friend json& operator >>(json& jsonData, Balance& _Balance)
	{
		_Balance.left = jsonData["left"];
		_Balance.right = jsonData["right"];
		return jsonData;
	}

	friend std::istream& operator>>(std::istream& is, Balance& _Balance)
	{
		is >> _Balance.left >> _Balance.right;
		return is;
	}

	Balance* checkAmount(std::string amount);
};

