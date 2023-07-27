#include "card.h"

card::card(std::string& _name, std::string& _surname, std::string& _patronomic,uint16_t& _dayOfBirth, uint16_t& _monthOfBirth,
	uint16_t& _yearOfBirth, uint16_t& _cardNumber, uint16_t& _CVV, uint16_t& _monthOfExpiry, uint16_t& _yearOfExpiry)
{
	this->ownerData = new personalData{ _name, _surname, _patronomic, _dayOfBirth, _monthOfBirth, _yearOfBirth };
	this->cardNumber = new uint16_t{ _cardNumber };
	this->CVV = new uint16_t{ _CVV };
	this->dateExpiry = new dateOfExpiry{ _monthOfExpiry, _yearOfExpiry };
}

uint16_t card::getCardNumber() const
{
	return *this->cardNumber;
}
uint16_t card::getCVV() const
{
	return *this->CVV;
}

card::~card()
{
	delete this->cardNumber;
	delete this->CVV;
	delete this->ownerData;
	delete this->dateExpiry;
}