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
		if (typeid(os) == typeid(std::ofstream))
		{
			os
				<< *_card.ownerData
				<< _card.cardNumber << std::endl
				<< *_card.CVV << std::endl
				<< *_card.dateExpiry
				<< *_card.balance << std::endl;
		}
		os
			<< *_card.ownerData
			<< "Card number: " << _card.cardNumber << std::endl
			<< "CVV: " << *_card.CVV << std::endl
			<< *_card.dateExpiry
			<< "Balance: " << * _card.balance << std::endl;
		return os;
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