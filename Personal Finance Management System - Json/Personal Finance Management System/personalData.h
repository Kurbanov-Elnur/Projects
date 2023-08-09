#include <iostream>
#include "nlohmann/json.hpp"

using json = nlohmann::json;
#pragma once

struct personalData
{
private:
	std::string name{};
	std::string surname{};
	std::string patronomic{};
	uint16_t* dayOfBirth{};
	uint16_t* monthOfBirth{};
	uint16_t* yearOfBirth{};
public:
	personalData(std::string&, std::string&, std::string&, uint16_t&, uint16_t&, uint16_t&);

	personalData();

	personalData(const personalData& other);

	friend std::ostream& operator <<(std::ostream& os, const personalData _personalData)
	{
		os
			<< "Owner name: " << _personalData.name << std::endl
			<< "Owner surname: " << _personalData.surname << std::endl
			<< "Owner patronomic: " << _personalData.patronomic << std::endl
			<< "Owner date of birth: " << *_personalData.dayOfBirth << '.' << *_personalData.monthOfBirth << '.' << *_personalData.yearOfBirth << std::endl;
		return os;
	}

	friend json operator <<(json& jsonData, const personalData _personalData)
	{
		jsonData["name"] = _personalData.name;
		jsonData["surname"] = _personalData.surname;
		jsonData["patronomic"] = _personalData.patronomic;
		jsonData["dayOfBirth"] = *_personalData.dayOfBirth;
		jsonData["monthOfBirth"] = *_personalData.monthOfBirth;
		jsonData["yearOfBirth"] = *_personalData.yearOfBirth;
		return jsonData;
	}

	friend json operator >> (json& jsonData, personalData& _personalData)
	{
		_personalData.name = jsonData["name"];
		_personalData.surname = jsonData["surname"];
		_personalData.patronomic = jsonData["patronomic"];
		*_personalData.dayOfBirth = jsonData["dayOfBirth"];
		*_personalData.monthOfBirth = jsonData["monthOfBirth"];
		*_personalData.yearOfBirth = jsonData["yearOfBirth"];
		return jsonData;
	}

	friend std::istream& operator>>(std::istream& is, personalData& _personalData)
	{
		is >> _personalData.name >> _personalData.surname >> _personalData.patronomic >> *_personalData.dayOfBirth >> *_personalData.monthOfBirth >> *_personalData.yearOfBirth;
		return is;
	}

	std::string getName() const;
	std::string getSurname() const; 
	std::string getPatronomic() const;
	uint16_t getDayOfBirth() const;
	uint16_t getMonthOfBirth() const;
	uint16_t getYearOfBirth() const;

	~personalData();
};
