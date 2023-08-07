#include "dateOfExpiry.h"

dateOfExpiry::dateOfExpiry(uint16_t& _monthOfExpiry, uint16_t& _yearOfExpiry)
{
	this->monthOfExpiry = new uint16_t{ _monthOfExpiry };
	this->yearOFExpiry = new uint16_t{ _yearOfExpiry };
}

dateOfExpiry::dateOfExpiry()
{
	this->monthOfExpiry = new uint16_t{};
	this->yearOFExpiry = new uint16_t{};
}

dateOfExpiry::dateOfExpiry(const dateOfExpiry& _other)
{
	this->monthOfExpiry = new uint16_t(*_other.monthOfExpiry);
	this->yearOFExpiry = new uint16_t(*_other.yearOFExpiry);
}

uint16_t dateOfExpiry::getMonthOfExpiry() const
{
	return *this->monthOfExpiry;
}

uint16_t dateOfExpiry::getYearOfExpiry() const
{
	return *this->yearOFExpiry;
}

dateOfExpiry::~dateOfExpiry()
{
	delete this->monthOfExpiry;
	delete this->yearOFExpiry;
}