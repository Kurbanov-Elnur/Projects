#include "personalData.h"

personalData::personalData(std::string& _name, std::string& _surname, std::string& _patronomic,
	uint16_t& _dayOfBirth, uint16_t& _monthOfBirth, uint16_t& _yearOfBirth)
{
	this->name = _name;
	this->surname = _surname;
	this->patronomic = _patronomic;
	*this->dayOfBirth = _dayOfBirth;
	*this->monthOfBirth = _monthOfBirth;
	*this->yearOfBirth = _yearOfBirth;
}

std::string personalData::getName() const
{
	return this->name;
}

std::string personalData::getSurname() const
{
	return this->surname;
}

std::string personalData::getPatronomic() const
{
	return this->patronomic;
}

uint16_t personalData::getDayOfBirth() const
{
	return *this->dayOfBirth;
}

uint16_t personalData::getMonthOfBirth() const
{
	return *this->monthOfBirth;
}

uint16_t personalData::getYearOfBirth() const
{
	return *this->yearOfBirth;
}

personalData::~personalData()
{
	delete this->dayOfBirth;
	delete this->monthOfBirth;
	delete this->yearOfBirth;
}
