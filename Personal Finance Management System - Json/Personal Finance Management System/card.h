#include "dateOfExpiry.h"
#include "personalData.h"
#include "Transaсtion.h"
#pragma once

class card
{
protected:
	std::string cardNumber{};
	uint16_t* CVV{};
public:
	personalData* ownerData{};
	dateOfExpiry* dateExpiry{};
	Balance* balance{};

	card(personalData&, std::string&, uint16_t&, Balance&, dateOfExpiry&);

	card();

	card(const card& _other);

	friend std::ostream& operator <<(std::ostream& os, const card _card)
	{
		os
			<< *_card.ownerData
			<< "Card number: " << _card.cardNumber << std::endl
			<< "CVV: " << *_card.CVV << std::endl
			<< *_card.dateExpiry
			<< "Balance: " << * _card.balance << std::endl;
		return os;
	}

	friend json& operator <<(json& jsonData, const card _card)
	{
		jsonData << *_card.ownerData;
		jsonData["cardNumber"] = _card.cardNumber;
		jsonData["CVV"] = *_card.CVV;
		jsonData << *_card.dateExpiry;
		jsonData << *_card.balance;

		return jsonData;
	}

	friend json& operator >>(json& jsonData, card& _card)
	{
		jsonData >> *_card.ownerData;
		_card.cardNumber = jsonData["cardNumber"];
		*_card.CVV = jsonData["CVV"];
		jsonData >> *_card.dateExpiry;
		jsonData >> *_card.balance;

		return jsonData;
	}
	
	friend std::istream& operator>>(std::istream& is, card& _card)
	{
		is >> *_card.ownerData >> _card.cardNumber >> *_card.CVV >> *_card.dateExpiry >> *_card.balance;
		return is;
	}

	std::string getCardNumber() const;
	uint16_t getCVV() const;
	Balance getBalance() const;
};