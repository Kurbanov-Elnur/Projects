#include <iostream>
#include <fstream>
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

	friend std::ostream& operator <<(std::ostream& os, const personalData _personalData)
	{
		if (typeid(os) == typeid(std::ofstream))
		{
			os
				<< _personalData.name << std::endl
				<< _personalData.surname << std::endl
				<< _personalData.patronomic << std::endl
				<< *_personalData.dayOfBirth << std::endl
				<< *_personalData.monthOfBirth << std::endl
				<< *_personalData.yearOfBirth << std::endl;
			return os;
		}
		os
			<< "Owner name: " << _personalData.name << std::endl
			<< "Owner surname: " << _personalData.surname << std::endl
			<< "Owner patronomic: " << _personalData.patronomic << std::endl
			<< "Owner date of birth: " << *_personalData.dayOfBirth << '.' << *_personalData.monthOfBirth << '.' << *_personalData.yearOfBirth << std::endl;
		return os;
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